using RabbitMQ.Client;

namespace ReportService.Infrastructure.MessageBroker.Channel
{
    public interface IChannelFactory : IAsyncDisposable
    {
        Task<IChannel> CreateAsync(CancellationToken ct = default);
    }
}
