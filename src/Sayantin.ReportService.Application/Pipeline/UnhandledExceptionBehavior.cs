using Microsoft.Extensions.Logging;

namespace ReportService.Application.Pipeline
{
    public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next(cancellationToken);
            }
            catch (Exception ex)
            {
                string message = $"Application Request Exception. Time={DateTime.Now} Request={typeof(TRequest).Name} Data={request} Exception:{ex}";
                _logger.LogError(message);

                throw;
            }
        }
    }
}