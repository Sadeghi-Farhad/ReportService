using ReportService.Application.Blogs.Handlers.BlogPublishedReply;
using ReportService.Infrastructure.Configurations;
using ReportService.Infrastructure.MessageBroker.Channel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReportService.Infrastructure.MessageBroker.Consumers
{
    public sealed class BlogReplyConsumer : BackgroundService
    {
        private readonly IChannelFactory _channelFactory;
        private IChannel? _channel = null;
        private readonly MessageBrokerSettings _settings;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BlogReplyConsumer> _logger;

        public BlogReplyConsumer(
            IChannelFactory channelFactory,
            IServiceScopeFactory scopeFactory,
             ILogger<BlogReplyConsumer> logger,
            IOptions<MessageBrokerSettings> options)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _channelFactory = channelFactory;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _channel = await _channelFactory.CreateAsync(ct);
            await _channel.BasicQosAsync(0, 5, false, ct);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (_, ea) =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    BlogPublishedReply? outcome = JsonConvert.DeserializeObject<BlogPublishedReply>(message);

                    if (outcome is null)
                    {
                        _logger.LogWarning("{RoutingKey} data is not valid.", ea.RoutingKey);
                        await _channel.BasicRejectAsync(ea.DeliveryTag, false, ct);
                        return;
                    }

                    // Update Blog
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(outcome, ct);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing BlogReply");
                    await _channel.BasicNackAsync(ea.DeliveryTag, false, false, ct);
                }
            };

            await _channel.BasicConsumeAsync(
                queue: _settings.BlogPublish_Queue_Reply,
                autoAck: true,
                consumer: consumer,
                cancellationToken: ct);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (_channel != null)
            {
                _channel.Dispose();
            }
            await base.StopAsync(cancellationToken);
        }
    }
}
