using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var jwtKey = builder.AddParameter("jwt-key", secret: true);
var jwtIssuer = builder.AddParameter("jwt-issuer");
var jwtAudience = builder.AddParameter("jwt-audience");
var jwtDuration = builder.AddParameter("jwt-duration");
var jwtKidDuration = builder.AddParameter("jwt-kid-duration");

var seedData = builder.AddParameter("seed-test-data");
var sensitiveDatabaseLogging = builder.AddParameter("database-log-sensitive-values");

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
    .WithEnvironment("SEED_TEST_DATA", seedData)
    .WithReference(dbConnection)
    .WaitFor(db);

var api = builder
    .AddProject<ChorePoint_API>("api")
    .WithHttpHealthCheck("/health")
    .WithEnvironment("JWT_KEY", jwtKey)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_DURATION", jwtDuration)
    .WithEnvironment("JWT_KID_DURATION", jwtKidDuration)
    .WithReference(redis)
    .WithReference(dbConnection)
    .WithReference(migrations)
    .WaitFor(redis)
    .WaitForCompletion(migrations);

builder
    .AddJavaScriptApp("website", "../../ChorePoint.Website")
    .WithHttpEndpoint(port: 4200, env: "PORT")
    .WithReference(api)
    .WaitFor(api);

await builder.Build().RunAsync();
