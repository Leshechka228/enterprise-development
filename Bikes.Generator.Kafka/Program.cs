using Bikes.Generator.Kafka;
using Bikes.Generator.Kafka.Services;
using Bikes.ServiceDefaults;

var builder = Host.CreateApplicationBuilder(args);

Console.WriteLine("=== Debug Info ===");
Console.WriteLine($"ConnectionStrings__bikes-kafka: {builder.Configuration.GetConnectionString("bikes-kafka")}");
Console.WriteLine($"Kafka:BootstrapServers: {builder.Configuration["Kafka:BootstrapServers"]}");
Console.WriteLine($"Kafka__BootstrapServers: {builder.Configuration["Kafka__BootstrapServers"]}");

Console.WriteLine("=== All Kafka-related env vars ===");
foreach (var env in Environment.GetEnvironmentVariables().Cast<System.Collections.DictionaryEntry>())
{
    var key = env.Key.ToString()!;
    if (key.Contains("kafka", StringComparison.OrdinalIgnoreCase) ||
        key.Contains("KAFKA", StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"{key} = {env.Value}");
    }
}
Console.WriteLine("==================");

builder.AddServiceDefaults();

builder.Services.AddSingleton<IDataGenerator, RandomDataGenerator>();
builder.Services.AddSingleton<IProducerService, KafkaGeneratorService>();
builder.Services.AddHostedService<KafkaProducerService>();

var host = builder.Build();
await host.RunAsync();