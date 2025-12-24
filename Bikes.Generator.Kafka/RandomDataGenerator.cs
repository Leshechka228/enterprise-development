using Bogus;
using Bikes.Application.Contracts.Bikes;
using Bikes.Application.Contracts.Models;
using Bikes.Application.Contracts.Renters;
using Bikes.Application.Contracts.Rents;

namespace Bikes.Generator.Kafka;

/// <summary>
/// Implementation of IDataGenerator using Bogus for generating random test data
/// </summary>
public class RandomDataGenerator : IDataGenerator
{
    private readonly Faker<BikeModelCreateUpdateDto> _bikeModelFaker;
    private readonly Faker<BikeCreateUpdateDto> _bikeFaker;
    private readonly Faker<RenterCreateUpdateDto> _renterFaker;
    private readonly Faker<RentCreateUpdateDto> _rentFaker;

    public RandomDataGenerator()
    {
        var bikeTypes = new[] { "Mountain", "Road", "Hybrid", "City", "Sport" };
        var brakeTypes = new[] { "Mechanical", "Hydraulic", "Rim" };
        var colors = new[] { "Red", "Blue", "Green", "Black", "White", "Yellow", "Silver" };

        _bikeModelFaker = new Faker<BikeModelCreateUpdateDto>()
            .RuleFor(m => m.Name, f => $"Model {f.Commerce.ProductName()}")
            .RuleFor(m => m.Type, f => f.PickRandom(bikeTypes))
            .RuleFor(m => m.WheelSize, f => f.Random.Decimal(10, 30))
            .RuleFor(m => m.MaxWeight, f => f.Random.Decimal(50, 300))
            .RuleFor(m => m.Weight, f => f.Random.Decimal(5, 50))
            .RuleFor(m => m.BrakeType, f => f.PickRandom(brakeTypes))
            .RuleFor(m => m.ModelYear, f => f.Random.Int(2000, 2024))
            .RuleFor(m => m.PricePerHour, f => f.Random.Decimal(1, 50));

        _bikeFaker = new Faker<BikeCreateUpdateDto>()
            .RuleFor(b => b.SerialNumber, f => $"SN{f.Random.AlphaNumeric(8).ToUpper()}")
            .RuleFor(b => b.ModelId, f => f.Random.Int(1, 100))
            .RuleFor(b => b.Color, f => f.PickRandom(colors));

        _renterFaker = new Faker<RenterCreateUpdateDto>()
            .RuleFor(r => r.FullName, f => f.Name.FullName())
            .RuleFor(r => r.Phone, f => f.Phone.PhoneNumber("+7##########"));

        _rentFaker = new Faker<RentCreateUpdateDto>()
            .RuleFor(r => r.BikeId, f => f.Random.Int(1, 1000))
            .RuleFor(r => r.RenterId, f => f.Random.Int(1, 500))
            .RuleFor(r => r.StartTime, f => f.Date.Recent(30))
            .RuleFor(r => r.DurationHours, f => f.Random.Int(1, 168));
    }

    public IEnumerable<BikeModelCreateUpdateDto> GenerateBikeModels(int count)
        => _bikeModelFaker.Generate(count);

    public IEnumerable<BikeCreateUpdateDto> GenerateBikes(int count, List<int> modelIds)
        => _bikeFaker.Generate(count);

    public IEnumerable<RenterCreateUpdateDto> GenerateRenters(int count)
        => _renterFaker.Generate(count);

    public IEnumerable<RentCreateUpdateDto> GenerateRents(int count, List<int> bikeIds, List<int> renterIds)
        => _rentFaker.Generate(count);
}