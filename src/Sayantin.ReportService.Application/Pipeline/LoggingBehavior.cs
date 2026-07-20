using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ReportService.Application.Pipeline
{
    public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;

        public LoggingBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            string message = $"Application Request. Time={DateTime.Now} Request={typeof(TRequest).Name} Data={request}";
            _logger.LogInformation(message);

            return Task.CompletedTask;
        }
    }
}