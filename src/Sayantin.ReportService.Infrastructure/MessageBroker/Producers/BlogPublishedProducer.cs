using ReportService.Domain.Blogs.Events;
using ReportService.Domain.Interfaces;
using ReportService.Infrastructure.Configurations;
using ReportService.Infrastructure.MessageBroker.Channel;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ReportService.Infrastructure.MessageBroker.Producers
{
    public sealed class BlogPublishedProducer : IBlogPublishedProducer, IAsyncDisposable
    {
        private readonly IChannelFactory _channelFactory;
        private IChannel? _channel = null;
        private readonly MessageBrokerSettings _settings;

        public BlogPublishedProducer(IChannelFactory channelFactory, IOptions<MessageBrokerSettings> options)
        {
            _channelFactory = channelFactory;
            _settings = options.Value;
        }

        public async Task PublishAsync(BlogPublishedEvent blog, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(blog.BlogTitle))
                throw new Exception("عنوان بلاگ مشخص نیست");

            _channel = await _channelFactory.CreateAsync(cancellationToken);

            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(blog));

            var props = new BasicProperties { DeliveryMode = DeliveryModes.Persistent }; // ماندگاری پیام در صورت ری‌استارت شدن سرور

            bool ReplyIsNeed = true;
            if (ReplyIsNeed) // اگر به جواب مصرف کننده پیام نیاز داشته باشیم
            {
                props.CorrelationId = blog.BlogId.ToString();
                props.ReplyTo = _settings.BlogPublish_Queue_Reply;

                await _channel.BasicPublishAsync(
                    exchange: _settings.Exchange,
                    routingKey: _settings.BlogPublish_Queue,
                    mandatory: true,
                    basicProperties: props,
                    body: body,
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _channel.BasicPublishAsync(
                    exchange: _settings.Exchange,
                    routingKey: _settings.BlogPublish_Queue,
                    mandatory: true,
                    basicProperties: props,
                    body: body,
                    cancellationToken: cancellationToken);
            }
        }


        public ValueTask DisposeAsync()
        {
            if (_channel != null)
            {
                _channel.Dispose();
            }
            return ValueTask.CompletedTask;
        }
    }
}
