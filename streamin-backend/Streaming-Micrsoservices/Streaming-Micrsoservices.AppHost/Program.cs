using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var password=builder.AddParameter("pg-password",secret:true);

var postgres = builder.AddPostgres("postgres")
    .WithPassword(password)
    .WithLifetime(ContainerLifetime.Session);

var rabbitmq = builder.AddRabbitMQ("rabbitmq")
    .WithLifetime(ContainerLifetime.Session)
    .WithManagementPlugin();

var elasticSearch = builder.AddElasticsearch("elasticSearch")
    .WithEnvironment("xpack.security.enabled", "false")
    .WithEnvironment("ES_JAVA_OPTS", "-Xms512m -Xmx512m")
    .WithLifetime(ContainerLifetime.Session);

//var identity_db = postgres.AddDatabase("identity_db");

var workservicedb = postgres.AddDatabase("workservicedb");
var identitydb = postgres.AddDatabase("identitydb");
var searchdb = postgres.AddDatabase("searchdb");
builder.AddProject<Projects.Identity_API>("identity-api")
    .WaitForStart(identitydb)
    .WithReference(identitydb)
    .WithReference(rabbitmq)
    .WaitForStart(rabbitmq)
    .WithEnvironment("ConnectionStrings__identitydb", identitydb);

var workservice=builder.AddProject<Projects.Work_Service_API>("work-service-api")
    .WaitForStart(workservicedb)
    .WithReference(workservicedb)
    .WaitFor(rabbitmq)
    .WithReference(rabbitmq);


builder.AddProject<Projects.SearchService_API>("searchservice-api")
    .WaitForStart(searchdb)
    .WithReference(searchdb)
    .WaitForStart(elasticSearch)
    .WithReference(elasticSearch)
    .WaitForStart(rabbitmq)
    .WithReference(rabbitmq)
    .WaitFor(workservice);


builder.Build().Run();
