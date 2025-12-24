using Microsoft.Extensions.Configuration;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres");
var bikesDb = postgres.AddDatabase("bikes-db");

var api = builder.AddProject<Projects.Bikes_Api_Host>("bikes-api")
    .WithReference(bikesDb)
    .WaitFor(bikesDb);

var kafka = builder.AddKafka("bikes-kafka")
    .WithKafkaUI();

var generatorSettings = builder.Configuration.GetSection("Generator");
var batchSize = generatorSettings.GetValue("BatchSize", 10);
var payloadLimit = generatorSettings.GetValue("PayloadLimit", 100);
var waitTime = generatorSettings.GetValue("WaitTime", 10000);

var kafkaSettings = builder.Configuration.GetSection("Kafka");
var topic = kafkaSettings["Topic"] ?? "bike-rents";
var groupId = kafkaSettings["GroupId"] ?? "bikes-consumer-group";

var consumer = builder.AddProject<Projects.Bikes_Infrastructure_Kafka>("bikes-consumer")
    .WithReference(kafka)
    .WithReference(bikesDb)
    .WaitFor(kafka)
    .WithEnvironment("Kafka__Topic", topic)
    .WithEnvironment("Kafka__GroupId", groupId)
    .WithEnvironment("ConnectionStrings__bikes-db", bikesDb);

var generator = builder.AddProject<Projects.Bikes_Generator_Kafka>("bikes-generator")
    .WithReference(kafka)
    .WaitFor(kafka)
    .WaitFor(api)
    .WaitFor(consumer)
    .WithEnvironment("Kafka__Topic", topic)
    .WithEnvironment("Generator__BatchSize", batchSize.ToString())
    .WithEnvironment("Generator__PayloadLimit", payloadLimit.ToString())
    .WithEnvironment("Generator__WaitTime", waitTime.ToString());

builder.Build().Run();