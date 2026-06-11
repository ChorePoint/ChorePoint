using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ChorePoint.Application.Behaviours;

public partial class LoggingBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        var requestGuid = Guid.NewGuid().ToString();

        var requestNameWithGuid = $"{requestName} [{requestGuid}]";

        LogRequestStart(requestNameWithGuid);
        TResponse response;

        var stopwatch = Stopwatch.StartNew();
        try
        {
            try
            {
                LogRequestInfoDebug(requestNameWithGuid, request);
            }
            catch (NotSupportedException ex)
            {
                logger.LogError(ex, "{RequestNameWithGuid} Could not serialise the request.",
                    requestNameWithGuid);
            }

            response = await next(cancellationToken);
        }
        finally
        {
            stopwatch.Stop();
            LogRequestEnd(requestNameWithGuid, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }


    [LoggerMessage(LogLevel.Information, "[START] {RequestNameWithGuid}")]
    partial void LogRequestStart(string requestNameWithGuid);

    [LoggerMessage(LogLevel.Debug, "[REQUEST_DEBUG] {RequestNameWithGuid} {Request}")]
    partial void LogRequestInfoDebug(string requestNameWithGuid, object request);

    [LoggerMessage(LogLevel.Information,
        "[END] {RequestNameWithGuid}; Execution time={StopwatchElapsedMilliseconds}ms")]
    partial void LogRequestEnd(string requestNameWithGuid, long stopwatchElapsedMilliseconds);
}