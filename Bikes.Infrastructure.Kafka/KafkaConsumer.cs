using Confluent.Kafka;
using Bikes.Application.Contracts.Rents;
using Bikes.Infrastructure.Kafka.Deserializers;

namespace Bikes.Infrastructure.Kafka;

/// <summary>
/// Background service that consumes rent contract messages from Kafka topics
/// </summary>
public class KafkaConsumer : BackgroundService
{
    private readonly IConsumer<Guid, IList<RentCreateUpdateDto>> _consumer;
    private readonly ILogger<KafkaConsumer> _logger;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly string _topic;
    private readonly string _bootstrapServers;

    public KafkaConsumer(
        IConfiguration configuration,
        ILogger<KafkaConsumer> logger,
        KeyDeserializer keyDeserializer,
        ValueDeserializer valueDeserializer,
        IServiceScopeFactory scopeFactory)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;

        var kafkaConfig = configuration.GetSection("Kafka");
        _topic = kafkaConfig["Topic"] ?? "bike-rents";

        _bootstrapServers = configuration.GetConnectionString("bikes-kafka")
            ?? configuration["Kafka:BootstrapServers"]
            ?? "localhost:9092";

        var groupId = kafkaConfig["GroupId"] ?? "bikes-consumer-group";

        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = _bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            AllowAutoCreateTopics = true,
            EnablePartitionEof = true,
            MaxPollIntervalMs = 300000,
            SessionTimeoutMs = 45000
        };

        _consumer = new ConsumerBuilder<Guid, IList<RentCreateUpdateDto>>(consumerConfig)
            .SetKeyDeserializer(keyDeserializer)
            .SetValueDeserializer(valueDeserializer)
            .SetErrorHandler((_, e) =>
            {
                if (e.IsFatal)
                    _logger.LogError("Kafka Fatal Error: {Reason} (Code: {Code})", e.Reason, e.Code);
                else
                    _logger.LogWarning("Kafka Error: {Reason} (Code: {Code})", e.Reason, e.Code);
            })
            .SetLogHandler((_, logMessage) =>
            {
                _logger.LogDebug("Kafka Log: {Facility} - {Message}", logMessage.Facility, logMessage.Message);
            })
            .Build();

        _logger.LogInformation("Kafka Consumer configured for: {Servers}", _bootstrapServers);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting Kafka consumer for topic: {Topic}", _topic);

        await WaitForKafkaAvailableAsync(stoppingToken);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                _consumer.Subscribe(_topic);
                _logger.LogInformation("Subscribed to Kafka topic: {Topic}", _topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        var result = _consumer.Consume(stoppingToken);

                        if (result == null)
                            continue;

                        if (result.IsPartitionEOF)
                        {
                            _logger.LogDebug("Reached end of partition {Partition}", result.Partition);
                            continue;
                        }

                        _logger.LogInformation(
                            "Received message {Key} with {Count} rents (Partition: {Partition}, Offset: {Offset})",
                            result.Message.Key,
                            result.Message.Value?.Count ?? 0,
                            result.Partition,
                            result.Offset
                        );

                        if (result.Message.Value != null)
                        {
                            await ProcessMessageAsync(result.Message.Key, result.Message.Value);
                        }
                    }
                    catch (ConsumeException ex) when (ex.Error.Code == ErrorCode.UnknownTopicOrPart)
                    {
                        _logger.LogWarning("Topic {Topic} not available yet, retrying in 5 seconds...", _topic);
                        await Task.Delay(5000, stoppingToken);
                        break;
                    }
                    catch (ConsumeException ex)
                    {
                        _logger.LogError(ex, "Consume error: {Reason} (Code: {Code})", ex.Error.Reason, ex.Error.Code);
                        await Task.Delay(2000, stoppingToken);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogInformation("Kafka consumer cancelled");
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in Kafka consumer");
                await Task.Delay(5000, stoppingToken);
            }
        }

        _logger.LogInformation("Kafka consumer stopped");
    }

    private async Task WaitForKafkaAvailableAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Waiting for Kafka to become available...");

        using var adminClient = new AdminClientBuilder(new AdminClientConfig
        {
            BootstrapServers = _bootstrapServers
        }).Build();

        for (var i = 0; i < 30; i++)
        {
            try
            {
                var metadata = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
                _logger.LogInformation("Kafka is available. Brokers: {BrokerCount}", metadata.Brokers.Count);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Kafka not available yet (attempt {Attempt}/30): {Message}", i + 1, ex.Message);
                await Task.Delay(5000, stoppingToken);
            }
        }

        _logger.LogWarning("Kafka still not available after 150 seconds, continuing anyway...");
    }

    private async Task ProcessMessageAsync(Guid key, IList<RentCreateUpdateDto> rents)
    {
        if (rents == null || rents.Count == 0)
        {
            _logger.LogWarning("Empty rents list for message {Key}", key);
            return;
        }

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var rentService = scope.ServiceProvider.GetRequiredService<IRentService>();

            await rentService.ReceiveContract(rents);

            _logger.LogInformation("Processed message {Key}: {Count} rent contracts saved",
                key, rents.Count);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing message {Key}", key);
        }
    }

    public override void Dispose()
    {
        try
        {
            _consumer?.Close();
            _consumer?.Dispose();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error disposing Kafka consumer");
        }

        base.Dispose();
        GC.SuppressFinalize(this);
    }
}