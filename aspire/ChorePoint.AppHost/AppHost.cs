using Projects;

using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

Log.Logger = new LoggerConfiguration().WriteTo.Console(theme: AnsiConsoleTheme.Code).CreateBootstrapLogger();

Log.Information("Starting ChorePoint.AppHost.csproj 〜(꒪꒳꒪)〜");

try
{
    var builder = DistributedApplication.CreateBuilder(args);

    var kidLoginCodeTimeout = builder.AddParameter("kid-login-code-timeout");

    var jwtKey = builder.AddParameter("jwt-key", secret: true);
    var jwtIssuer = builder.AddParameter("jwt-issuer");
    var jwtAudience = builder.AddParameter("jwt-audience");
    var jwtDuration = builder.AddParameter("jwt-duration");
    var jwtKidDuration = builder.AddParameter("jwt-kid-duration");
    var kidLoginCodeLength = builder.AddParameter("kid-login-code-length");

    var enableCaching = builder.AddParameter("enable-caching");

    var sensitiveDatabaseLogging = builder.AddParameter("database-log-sensitive-values");
    var enableMigrationService = builder.AddParameter("enable-migration-service");
    var seedTestData = builder.AddParameter("seed-test-data");

    var sensitiveDatabaseLoggingValue = bool.Parse(await sensitiveDatabaseLogging.Resource.GetValueAsync(CancellationToken.None)
                                              ?? throw new InvalidOperationException("database-log-sensitive-values must be set to either true or false"));
    var enableMigrationServiceValue = bool.Parse(await enableMigrationService.Resource.GetValueAsync(CancellationToken.None)
                                                 ?? throw new InvalidOperationException("enable-migration-service must be set to either true or false"));
    var seedTestDataValue = bool.Parse(await seedTestData.Resource.GetValueAsync(CancellationToken.None)
                                       ?? throw new InvalidOperationException("seed-test-data must be set to either true or false"));

    var postgres = builder.AddPostgres("postgres").WithDbGate();
    var redis = builder.AddRedis("redis").WithRedisCommander();
    if (!seedTestDataValue)
    {
        postgres.WithDataVolume();
        redis.WithDataVolume();
    }
    else
    {
        if (!enableMigrationServiceValue)
        {
            throw new InvalidOperationException("Cannot enable test data seeding without also enabling ChorePoint.MigrationService");
        }
    }

    var connectionStringAdditions = string.Empty;
    if (sensitiveDatabaseLoggingValue)
    {
        connectionStringAdditions = "Include Error Detail=true;Log Parameters=true";
    }

    var db = postgres.AddDatabase("database");
    var dbConnection = builder
        .AddConnectionString("database-connection", ReferenceExpression.Create($"{db};{connectionStringAdditions}"))
        .WaitFor(db);

    var api = builder
        .AddProject<ChorePoint_API>("api")
        .WithHttpHealthCheck("/health")
        .WithEnvironment("Api__KidLoginCodeTimeout", kidLoginCodeTimeout)
        .WithEnvironment("Authentication__JwtKey", jwtKey)
        .WithEnvironment("Authentication__JwtIssuer", jwtIssuer)
        .WithEnvironment("Authentication__JwtAudience", jwtAudience)
        .WithEnvironment("Authentication__JwtDuration", jwtDuration)
        .WithEnvironment("Authentication__JwtKidDuration", jwtKidDuration)
        .WithEnvironment("Authentication__KidLoginCodeLength", kidLoginCodeLength)
        .WithEnvironment("Cache__EnableCaching", enableCaching)
        .WithEnvironment("Database__EnableSensitiveLogging", sensitiveDatabaseLogging)
        .WithReference(dbConnection)
        .WithReference(redis)
        .WaitFor(redis);

    api.WithUrls(context =>
    {
        foreach (var url in context.Urls.Where(url => string.IsNullOrEmpty(url.DisplayText)).ToList())
        {
            if (url.Endpoint is null)
            {
                continue;
            }

            url.DisplayText = $"Scalar ({url.Endpoint.Scheme.ToUpper()})";

            if (url.DisplayLocation is UrlDisplayLocation.SummaryAndDetails)
            {
                context.Urls.Add(new ResourceUrlAnnotation
                {
                    Url = $"{url.Url[..url.Url.IndexOf("/scalar", StringComparison.Ordinal)]}/hangfire",
                    DisplayText = $"Hangfire ({url.Endpoint.Scheme.ToUpper()})"
                });
            }
        }
    });

    api.AddEFMigrations("ef-helper", "ChorePoint.Infrastructure.AppDbContext")
        .WithMigrationsProject<ChorePoint_Infrastructure>();

    if (enableMigrationServiceValue)
    {
        var migrationService = builder
            .AddProject<ChorePoint_MigrationService>("migration-service")
            .WithEnvironment("Database__EnableSensitiveLogging", sensitiveDatabaseLogging)
            .WithEnvironment("Database__SeedTestData", seedTestData)
            .WithReference(dbConnection)
            .WaitFor(db);

        api.WithReference(migrationService);
        api.WaitForCompletion(migrationService);
    }
    else
    {
        api.WaitFor(db);
    }

    builder
        .AddJavaScriptApp("website", "../../ChorePoint.Website")
        .WithHttpEndpoint(port: 4200, env: "PORT")
        .WithEnvironment("Authentication__KidLoginCodeLength", kidLoginCodeLength)
        .WithReference(api)
        .WaitFor(api);

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "ChorePoint.AppHost.csproj startup terminated unexpectedly... ⎛⎝( ` ᢍ ´ )⎠⎞ᵐᵘʰᵃʰᵃ");
}
finally
{
    Log.Information("Bye bye... (ㄒoㄒ)");
    await Log.CloseAndFlushAsync();
}
