using MediatR;
using Microsoft.Extensions.Logging;

namespace Demo.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                LogException(ex, LogLevel.Error, request);
                throw;
            }
        }

        private void LogException(Exception ex, LogLevel level, TRequest request)
        {
            _logger.Log(
                level,
                ex,
                "Error: {ExceptionType} for Request {Name} {@Request}",
                ex.GetType().Name,
                typeof(TRequest).Name,
                request);
        }
    }
}
