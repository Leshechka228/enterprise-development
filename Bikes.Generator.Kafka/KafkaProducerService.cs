using Bikes.Generator.Kafka.Services;

namespace Bikes.Generator.Kafka;

/// <summary>
/// Background service for generating and sending a specified number of contracts at specified intervals
/// </summary>
public class KafkaProducerService : BackgroundService
{
    private readonly int _batchSize;
    private readonly int _payloadLimit;
    private readonly int _waitTime;
    private readonly IProducerService _producer;
    private readonly ILogger<KafkaProducerService> _logger;
    private readonly IDataGenerator _dataGenerator;

    public KafkaProducerService(
        IConfiguration configuration,
        IProducerService producer,
        ILogger<KafkaProducerService> logger,
        IDataGenerator dataGenerator)
    {
        _batchSize = configuration.GetValue<int>("Generator:BatchSize", 10);
        _payloadLimit = configuration.GetValue<int>("Generator:PayloadLimit", 100);
        _waitTime = configuration.GetValue<int>("Generator:WaitTime", 10000);

        if (_batchSize <= 0)
            throw new ArgumentException($"Invalid argument value for BatchSize: {_batchSize}");
        if (_payloadLimit <= 0)
            throw new ArgumentException($"Invalid argument value for PayloadLimit: {_payloadLimit}");
        if (_waitTime <= 0)
            throw new ArgumentException($"Invalid argument value for WaitTime: {_waitTime}");

        _producer = producer;
        _logger = logger;
        _dataGenerator = dataGenerator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Waiting 30 seconds for Kafka to become fully ready...");
        await Task.Delay(60000, stoppingToken);

        _logger.LogInformation("Starting to send {total} messages with {time}s interval with {batch} messages in batch",
            _payloadLimit, _waitTime / 1000, _batchSize);

        var counter = 0;
        while (counter < _payloadLimit && !stoppingToken.IsCancellationRequested)
        {
            try
            {
                await GenerateAndSendBatchAsync();
                counter += _batchSize;
                _logger.LogInformation("Sent {sent} messages, total: {total}/{limit}",
                    _batchSize, counter, _payloadLimit);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Send batch with error. Retry");
            }

            await Task.Delay(_waitTime, stoppingToken);
        }

        _logger.LogInformation("Finished sending {total} messages with {time}s interval with {batch} messages in batch",
            _payloadLimit, _waitTime / 1000, _batchSize);
    }

    private async Task GenerateAndSendBatchAsync()
    {
        // Generate required data for rents
        var models = _dataGenerator.GenerateBikeModels(_batchSize / 4).ToList();
        var renters = _dataGenerator.GenerateRenters(_batchSize / 4).ToList();
        var modelIds = Enumerable.Range(1, models.Count).ToList();
        var renterIds = Enumerable.Range(1, renters.Count).ToList();
        var bikes = _dataGenerator.GenerateBikes(_batchSize / 4, modelIds).ToList();
        var bikeIds = Enumerable.Range(1, bikes.Count).ToList();

        // Generate only rents
        var rents = _dataGenerator.GenerateRents(_batchSize, bikeIds, renterIds).ToList();

        // Send list of rents
        await _producer.SendAsync("bike-rents", rents);

        _logger.LogInformation("Generated {RentCount} rents", rents.Count);
    }
}