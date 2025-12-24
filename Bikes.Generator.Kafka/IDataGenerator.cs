namespace Bikes.Generator.Kafka;

/// <summary>
/// Interface for generating random test data
/// </summary>
public interface IDataGenerator
{
    public IEnumerable<Bikes.Application.Contracts.Models.BikeModelCreateUpdateDto> GenerateBikeModels(int count);
    public IEnumerable<Bikes.Application.Contracts.Bikes.BikeCreateUpdateDto> GenerateBikes(int count, List<int> modelIds);
    public IEnumerable<Bikes.Application.Contracts.Renters.RenterCreateUpdateDto> GenerateRenters(int count);
    public IEnumerable<Bikes.Application.Contracts.Rents.RentCreateUpdateDto> GenerateRents(int count, List<int> bikeIds, List<int> renterIds);
}