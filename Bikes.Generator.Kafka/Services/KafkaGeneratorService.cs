using Bikes.Generator.Kafka.Serializers;
using Confluent.Kafka;
using Polly;
using Bikes.Application.Contracts.Rents;

namespace Bikes.Generator.Kafka.Services;

/// <summary>
/// Service for producing messages to Kafka with retry logic and error handling
/// </summary>
public class KafkaGeneratorService : IProducerService, IDisposable
{
    private readonly ILogger<KafkaGeneratorService> _logger;
    private readonly IProducer<Guid, IList<RentCreateUpdateDto>> _producer;
    private readonly int _retryCount;
    private readonly int _retryDelayMs;
    private bool _disposed = false;

    public KafkaGeneratorService(
        ILogger<KafkaGeneratorService> logger,
        IConfiguration configuration)
    {
        _logger = logger;

        var bootstrapServers = configuration.GetConnectionString("bikes-kafka")
            ?? configuration["Kafka:BootstrapServers"]
            ?? configuration["Kafka__BootstrapServers"]
            ?? "localhost:9092";

        _logger.LogInformation("Kafka BootstrapServers: {BootstrapServers}", bootstrapServers);

        var config = new ProducerConfig
        {
            BootstrapServers = bootstrapServers,
            MessageSendMaxRetries = 20,
            RetryBackoffMs = 10000,
            SocketTimeoutMs = 60000,
            MessageTimeoutMs = 120000,
            EnableDeliveryReports = true,
            Acks = Acks.All,
            SecurityProtocol = SecurityProtocol.Plaintext
        };

        _producer = new ProducerBuilder<Guid, IList<RentCreateUpdateDto>>(config)
            .SetKeySerializer(new KeySerializer())
            .SetValueSerializer(new ValueSerializer())
            .SetLogHandler((_, message) =>
            {
                _logger.LogDebug("Kafka log: {Facility} {Message}", message.Facility, message.Message);
            })
            .SetErrorHandler((_, error) =>
            {
                if (error.IsFatal)
                    _logger.LogError("Kafka fatal error: {Reason} (Code: {Code})", error.Reason, error.Code);
                else
                    _logger.LogWarning("Kafka error: {Reason} (Code: {Code})", error.Reason, error.Code);
            })
            .Build();

        _retryCount = configuration.GetValue<int>("Kafka:RetryCount", 10);
        _retryDelayMs = configuration.GetValue<int>("Kafka:RetryDelayMs", 5000);
    }

    public async Task SendAsync(string topic, IList<RentCreateUpdateDto> items)
    {
        if (items == null || items.Count == 0)
            return;

        var retryPolicy = Policy
            .Handle<KafkaException>()
            .WaitAndRetryAsync(
                _retryCount,
                attempt => TimeSpan.FromMilliseconds(_retryDelayMs * Math.Pow(2, attempt - 1)),
                onRetry: (exception, delay, attempt, context) =>
                {
                    _logger.LogWarning(exception,
                        "Retry {Attempt}/{MaxAttempts} after {Delay}ms",
                        attempt, _retryCount, delay.TotalMilliseconds);
                });

        await retryPolicy.ExecuteAsync(async () =>
        {
            var key = Guid.NewGuid();

            var message = new Message<Guid, IList<RentCreateUpdateDto>>
            {
                Key = key,
                Value = items
            };

            var result = await _producer.ProduceAsync(topic, message);
            _logger.LogInformation("Sent {Count} items to Kafka topic {Topic} (Partition: {Partition}, Offset: {Offset})",
                items.Count, topic, result.Partition, result.Offset);
        });
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _producer?.Flush(TimeSpan.FromSeconds(5));
            _producer?.Dispose();
            _disposed = true;
        }
        GC.SuppressFinalize(this);
    }
}