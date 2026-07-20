using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ReportService.Application.Pipeline
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;

        public PerformanceBehavior(ILogger<TRequest> logger)
        {
            _timer = new Stopwatch();
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();

            var response = await next(cancellationToken);

            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 1000)
            {
                string message = $"Application Request Long Response Time. Time={DateTime.Now} Request={typeof(TRequest).Name} Duration={_timer.ElapsedMilliseconds}(ms) Data={request}";
                _logger.LogWarning(message);
            }

            return response;
        }
    }
}