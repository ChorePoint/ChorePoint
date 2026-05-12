using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChorePoint.Application.Behaviours;

public class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request?.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();

        var requestNameWithGuid = $"{requestName} [{requestGuid}]";

        logger.LogInformation("[START] {RequestNameWithGuid}", requestNameWithGuid);
        TResponse response;

        var stopwatch = Stopwatch.StartNew();
        try
        {
            try
            {
                logger.LogDebug("[REQUEST_DEBUG] {RequestNameWithGuid} {Request}", requestNameWithGuid, request);
            }
            catch (NotSupportedException ex)
            {
                logger.LogError("{RequestNameWithGuid} Could not serialise the request due to: {ExceptionMessage}",
                    requestNameWithGuid, ex.Message);
            }

            response = await next(cancellationToken);
        }
        finally
        {
            stopwatch.Stop();
            logger.LogInformation("[END] {RequestNameWithGuid}; Execution time={StopwatchElapsedMilliseconds}ms",
                requestNameWithGuid, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}