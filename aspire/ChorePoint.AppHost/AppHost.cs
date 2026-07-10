using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres").WithPgAdmin().WithDataVolume();

var db = postgres.AddDatabase("chorepoint-db");

var migrations = builder
    .AddProject<ChorePoint_MigrationService>("migrations")
    .WithReference(db)
    .WaitFor(db);

var api = builder
    .AddProject<ChorePoint_API>("api")
    .WithHttpHealthCheck("/health")
    .WithReference(db)
    .WithReference(migrations)
    .WaitForCompletion(migrations);

var website = builder
    .AddJavaScriptApp("website", "../../ChorePoint.Website")
    .WithHttpEndpoint(env: "PORT")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
