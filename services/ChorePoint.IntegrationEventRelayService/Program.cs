using ChorePoint.Application;
using ChorePoint.Infrastructure.BuilderExtensions;
using ChorePoint.IntegrationEventRelayService.Workers;
using ChorePoint.ServiceDefaults;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateBootstrapLogger();

Log.Information("We about to start sorting out all these INTEGRATION EVENTS in the OUTBOX!! ヽ(ﾟДﾟ)ﾉ");

try
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.Services.AddOpenTelemetry().WithTracing(tracing =>
    {
        tracing.AddSource(PublishWaitingIntegrationEventsWorker.ActivitySourceName);
        tracing.AddSource(PurgeCompletedIntegrationEventsWorker.ActivitySourceName);
        tracing.AddSource(RetryFailedIntegrationEventsWorker.ActivitySourceName);
    });

    builder.AddDatabase();

    var services = builder.Services;

    services.AddMediatR();

    services.AddHostedService<PublishWaitingIntegrationEventsWorker>();
    services.AddHostedService<PurgeCompletedIntegrationEventsWorker>();
    services.AddHostedService<RetryFailedIntegrationEventsWorker>();

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "ChorePoint.IntegrationEventRelayService startup terminated unexpectedly, the integration events are not bussin （╯°□°）╯︵( .o.)");
}
finally
{
    Log.Information("All the integration events have been told off, and are now behaving correctly ┬──┬ ノ( ゜-゜ノ)");
    await Log.CloseAndFlushAsync();
}
