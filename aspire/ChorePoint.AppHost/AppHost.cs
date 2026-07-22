using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var jwtKey = builder.AddParameter("jwt-key", secret: true);
var jwtIssuer = builder.AddParameter("jwt-issuer");
var jwtAudience = builder.AddParameter("jwt-audience");
var jwtDuration = builder.AddParameter("jwt-duration");

var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume();

var db = postgres.AddDatabase("chorepoint-db");

var migrations = builder
    .AddProject<ChorePoint_MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(db);

var api = builder
    .AddProject<ChorePoint_API>("api")
    .WithHttpHealthCheck("/health")
    .WithEnvironment("JWT_KEY", jwtKey)
    .WithEnvironment("JWT_ISSUER", jwtIssuer)
    .WithEnvironment("JWT_AUDIENCE", jwtAudience)
    .WithEnvironment("JWT_DURATION", jwtDuration)
    .WithReference(db)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

var website = builder
    .AddJavaScriptApp("website", "../../ChorePoint.Website")
    .WithHttpEndpoint(env: "PORT")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
