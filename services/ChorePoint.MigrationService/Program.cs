using ChorePoint.Domain.Entities;
using ChorePoint.Infrastructure;
using ChorePoint.MigrationService;
using ChorePoint.ServiceDefaults;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: AnsiConsoleTheme.Code)
    .CreateBootstrapLogger();

Log.Information("Everyone get ready, we are about to migrate!!");

try
{
    var builder = Host.CreateApplicationBuilder(args);

    builder.AddServiceDefaults();

    if (
        bool.TryParse(Environment.GetEnvironmentVariable("SEED_TEST_DATA"), out var seedData)
        && seedData
    )
        builder.Services.AddScoped<PasswordHasher<Parent>>();

    builder.Services.AddHostedService<Worker>();

    builder
        .Services.AddOpenTelemetry()
        .WithTracing(tracing => tracing.AddSource(Worker.ActivitySourceName));

    builder.AddNpgsqlDbContext<AppDbContext>("chorepoint-db-cs");

    var host = builder.Build();
    await host.RunAsync();
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
