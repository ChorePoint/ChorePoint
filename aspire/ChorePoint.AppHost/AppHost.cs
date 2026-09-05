using Projects;

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
var seedData = builder.AddParameter("seed-test-data");

var postgres = builder.AddPostgres("postgres").WithDbGate();
var redis = builder.AddRedis("redis").WithRedisCommander();
if (bool.TryParse(await seedData.Resource.GetValueAsync(CancellationToken.None), out var seedDataValue) && !seedDataValue)
{
    postgres.WithDataVolume();
    redis.WithDataVolume();
}

var connectionStringAdditions = string.Empty;
if (
    bool.TryParse(
        await sensitiveDatabaseLogging.Resource.GetValueAsync(CancellationToken.None),
        out var sensitiveDatabaseLoggingValue
    ) && sensitiveDatabaseLoggingValue
)
{
    connectionStringAdditions = "Include Error Detail=true;Log Parameters=true";
}

var db = postgres.AddDatabase("database");
var dbConnection = builder
    .AddConnectionString("database-connection", ReferenceExpression.Create($"{db};{connectionStringAdditions}"))
    .WaitFor(db);

var migrations = builder
    .AddProject<ChorePoint_MigrationService>("migrations")
    .WithEnvironment("Database__EnableSensitiveLogging", sensitiveDatabaseLogging)
    .WithEnvironment("Database__SeedTestData", seedData)
    .WithReference(dbConnection)
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
    .WithReference(redis)
    .WithReference(dbConnection)
    .WithReference(migrations)
    .WaitFor(redis)
    .WaitForCompletion(migrations);

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

builder
    .AddJavaScriptApp("website", "../../ChorePoint.Website")
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithEnvironment("Authentication__KidLoginCodeLength", kidLoginCodeLength)
    .WithReference(api)
    .WaitFor(api);

await builder.Build().RunAsync();
