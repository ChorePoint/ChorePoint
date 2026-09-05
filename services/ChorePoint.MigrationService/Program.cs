using ChorePoint.Infrastructure.ServiceExtensions;
using ChorePoint.MigrationService;
using ChorePoint.ServiceDefaults;

using Microsoft.AspNetCore.Identity;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateBootstrapLogger();

Log.Information("Everyone get ready, we are about to migrate!!");

try
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();
    builder.Services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

    builder.AddDatabase();

    builder.Services.AddScoped<PasswordHasher<string>>();

    builder.Services.AddHostedService<Worker>();

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "MigrationService startup terminated unexpectedly");
}
finally
{
    Log.Information("DAMN, we actually migrated, WOOO!");
    await Log.CloseAndFlushAsync();
}
