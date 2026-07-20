using ReportService.Infrastructure.Configurations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace ReportService.Infrastructure.MessageBroker.Channel
{
    public sealed class ChannelFactory : IChannelFactory
    {
        private readonly MessageBrokerSettings _settings;
        private IConnection? _connection;
        private readonly SemaphoreSlim _lock = new(1, 1);

        public ChannelFactory(IOptions<MessageBrokerSettings> options, IServiceScopeFactory scopeFactory)
        {
            _settings = options.Value;
        }

        private async Task<IConnection> GetConnectionAsync(CancellationToken ct = default)
        {
            if (_connection is not null && _connection.IsOpen)
                return _connection;

            await _lock.WaitAsync(ct);
            try
            {
                if (_connection is not null && _connection.IsOpen)
                    return _connection;

                var factory = new ConnectionFactory
                {
                    HostName = _settings.Host,
                    Port = _settings.Port,
                    UserName = _settings.UserName,
                    Password = _settings.Password,
                    VirtualHost = _settings.VirtualHost,
                    AutomaticRecoveryEnabled = true,
                    NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
                };

                _connection = await factory.CreateConnectionAsync(ct);

                return _connection;
            }
            finally
            {
                _lock.Release();
            }
        }

        public async Task<IChannel> CreateAsync(CancellationToken ct = default)
        {
            var conn = await GetConnectionAsync(ct);
            var channel = await conn.CreateChannelAsync();

            return channel;
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection is not null && _connection.IsOpen)
                await _connection.CloseAsync();
        }
    }
}
