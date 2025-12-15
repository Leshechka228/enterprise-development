using Aspire.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var bikesDb = postgres.AddDatabase("bikes-db");

builder.AddProject<Projects.Bikes_Api_Host>("bikes-api")
    .WithReference(bikesDb)
    .WaitFor(bikesDb);

builder.Build().Run();