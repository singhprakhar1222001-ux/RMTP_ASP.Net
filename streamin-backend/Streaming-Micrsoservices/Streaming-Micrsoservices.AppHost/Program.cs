using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var password=builder.AddParameter("pg-password",secret:true);
var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPassword(password)
    .WithLifetime(ContainerLifetime.Persistent);
//var identity_db = postgres.AddDatabase("identity_db");

var workservicedb = postgres.AddDatabase("workservicedb");
var identitydb = postgres.AddDatabase("identitydb");

builder.AddProject<Projects.Identity_API>("identity-api")
    .WaitForStart(identitydb)
    .WithReference(identitydb)
    .WithEnvironment("ConnectionStrings__identitydb", identitydb);

builder.AddProject<Projects.Work_Service_API>("work-service-api")
    .WithReference(workservicedb);


builder.Build().Run();
