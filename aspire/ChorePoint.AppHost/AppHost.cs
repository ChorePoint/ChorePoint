using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var jwtKey = builder.AddParameter("jwt-key", secret: true);
var jwtIssuer = builder.AddParameter("jwt-issuer");
var jwtAudience = builder.AddParameter("jwt-audience");
var jwtDuration = builder.AddParameter("jwt-duration");

var seedData = builder.AddParameter("seed-test-data");
var sensitiveDatabaseLogging = builder.AddParameter("database-log-sensitive-values");

var postgres = builder.AddPostgres("postgres").WithDbGate();
if (
    bool.TryParse(
        await seedData.Resource.GetValueAsync(CancellationToken.None),
        out var seedDataValue
    ) && !seedDataValue
)
    postgres.WithDataVolume();

var connectionStringAdditions = string.Empty;
if (
    bool.TryParse(
        await sensitiveDatabaseLogging.Resource.GetValueAsync(CancellationToken.None),
        out var sensitiveDatabaseLoggingValue
    ) && sensitiveDatabaseLoggingValue
)
    connectionStringAdditions = "Include Error Detail=true;Log Parameters=true";

var db = postgres.AddDatabase("chorepoint-db");
var dbConnection = builder
    .AddConnectionString(
        "chorepoint-db-cs",
        ReferenceExpression.Create($"{db};{connectionStringAdditions}")
    )
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
    .WithReference(dbConnection)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

builder
    .AddJavaScriptApp("website", "../../ChorePoint.Website")
    .WithHttpEndpoint(env: "PORT")
    .WithReference(api)
    .WaitFor(api);

await builder.Build().RunAsync();
