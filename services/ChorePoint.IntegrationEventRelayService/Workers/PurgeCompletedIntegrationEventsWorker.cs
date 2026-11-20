using System.Diagnostics;

namespace ChorePoint.IntegrationEventRelayService.Workers;

public class PurgeCompletedIntegrationEventsWorker(ILogger<PurgeCompletedIntegrationEventsWorker> logger) : BackgroundService
{
    public const string ActivitySourceName = "Purge Completed Integration Events";
    private static readonly ActivitySource ActivitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (logger.IsEnabled(LogLevel.Information))
            {
                logger.LogInformation("PurgeCompletedIntegrationEventsWorker running at: {Time}", DateTimeOffset.Now);
            }

            await Task.Delay(1000, stoppingToken);
        }
    }
}
