using ReportService.Application.Users.Commands.CreateUser;
using ReportService.Infrastructure.Configurations;
using ReportService.Infrastructure.MessageBroker.Channel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReportService.Infrastructure.MessageBroker.Consumers
{
    public class UserCreatedConsumerHostedService : BackgroundService
    {
        private readonly IChannelFactory _channelFactory;
        private IChannel? _channel = null;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly MessageBrokerSettings _settings;

        public UserCreatedConsumerHostedService(
            IChannelFactory channel,
            IServiceScopeFactory scopeFactory,
            IOptions<MessageBrokerSettings> options)
        {
            _channelFactory = channel;
            _scopeFactory = scopeFactory;
            _settings = options.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _channel = await _channelFactory.CreateAsync(ct);
            await _channel.BasicQosAsync(0, 5, false, ct);

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, ea) =>
            {
                var retryCount =
                    ea.BasicProperties.Headers?.TryGetValue("x-retry-count", out var value) == true
                        ? Convert.ToInt32(value)
                        : 0;

                int userId = int.Parse(ea.BasicProperties.CorrelationId ?? "0");
                string? replyTo = ea.BasicProperties.ReplyTo;

                UserReplyDto reply = new()
                {
                    RetryCount = retryCount + 1,
                    Status = string.Empty
                };

                var props = new BasicProperties { DeliveryMode = DeliveryModes.Persistent }; // ماندگاری پیام در صورت ری‌استارت شدن سرور

                try
                {
                    var command = JsonConvert.DeserializeObject<CreateUserCommand>(
                        Encoding.UTF8.GetString(ea.Body.ToArray()));

                    #region AddUser
                    using var scope = _scopeFactory.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await mediator.Send(command!, ct);
                    #endregion Add User

                    reply.Status = "success";

                    if (!string.IsNullOrEmpty(replyTo))
                        await _channel.BasicPublishAsync(
                            exchange: _settings.Exchange,
                            routingKey: replyTo,
                            mandatory: true,
                            basicProperties: props,
                            body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reply)), ct);

                    await _channel.BasicAckAsync(ea.DeliveryTag, false, ct);
                }
                catch
                {
                    if (reply.RetryCount >= 5)
                    {
                        //Fail
                        reply.Status = "fail";
                        if (!string.IsNullOrEmpty(replyTo))
                            await _channel.BasicPublishAsync(
                                exchange: _settings.Exchange,
                                routingKey: replyTo,
                                mandatory: true,
                                basicProperties: props,
                                body: Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reply)));

                        await _channel.BasicNackAsync(ea.DeliveryTag, false, requeue: false, ct);
                    }
                    else
                    {
                        reply.Status = "retry";

                        if (!string.IsNullOrEmpty(replyTo))
                            await _channel.BasicPublishAsync(_settings.Exchange, replyTo, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(reply)));

                        props = new BasicProperties
                        {
                            CorrelationId = ea.BasicProperties.CorrelationId,
                            ReplyTo = ea.BasicProperties.ReplyTo,
                            DeliveryMode = DeliveryModes.Persistent,
                            Headers = new Dictionary<string, object?>
                            {
                                ["x-retry-count"] = retryCount + 1
                            }
                        };

                        await _channel.BasicPublishAsync(
                            exchange: _settings.Exchange,
                           routingKey: _settings.UserCreated_Queue_Delayed,
                           mandatory: true,
                           basicProperties: props,
                           body: ea.Body,
                           cancellationToken: ct);

                        await _channel.BasicAckAsync(ea.DeliveryTag, false, ct);
                    }
                }
            };

            await _channel.BasicConsumeAsync(
                queue: _settings.UserCreated_Queue,
                autoAck: false,
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