var builder = DistributedApplication.CreateBuilder(args);


var postgres = builder.AddPostgres("postgres")
    .WithHostPort(5432)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent);

var identity_db = postgres.AddDatabase("identity_db");

builder.AddProject<Projects.Identity_API>("identity-api")
    .WithReference(identity_db);

builder.Build().Run();
