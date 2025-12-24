using Bikes.Application.Contracts.Rents;

namespace Bikes.Generator.Kafka.Services;

public interface IProducerService
{
    public Task SendAsync(string topic, IList<RentCreateUpdateDto> items);
}