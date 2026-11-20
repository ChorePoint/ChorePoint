using System.Diagnostics;

namespace ChorePoint.IntegrationEventRelayService.Workers;

public class RetryFailedIntegrationEventsWorker(ILogger<RetryFailedIntegrationEventsWorker> logger) : BackgroundService
{
    public const string ActivitySourceName = "Retry Failed Integration Events";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("RetryFailedIntegrationEventsWorker running at: {Time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
