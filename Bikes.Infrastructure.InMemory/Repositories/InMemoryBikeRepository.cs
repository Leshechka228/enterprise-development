using Bikes.Domain.Models;
using Bikes.Domain.Repositories;
using Bikes.Tests;

namespace Bikes.Infrastructure.InMemory.Repositories;

/// <summary>
/// In-memory implementation of bike repository
/// </summary>
public class InMemoryBikeRepository() : IBikeRepository
{
    private readonly List<Bike> _bikes = [.. new BikesFixture().Bikes];
    private readonly List<BikeModel> _models = [.. new BikesFixture().Models];
    private readonly List<Rent> _rents = [.. new BikesFixture().Rents];
    private readonly List<Renter> _renters = [.. new BikesFixture().Renters];

    // Bike methods
    public List<Bike> GetAllBikes() => [.. _bikes];

    public Bike? GetBikeById(int id) => _bikes.FirstOrDefault(b => b.Id == id);

    public void AddBike(Bike bike)
    {
        ArgumentNullException.ThrowIfNull(bike);
        _bikes.Add(bike);
    }

    public void UpdateBike(Bike bike)
    {
        ArgumentNullException.ThrowIfNull(bike);
        var existingBike = _bikes.FirstOrDefault(b => b.Id == bike.Id);
        if (existingBike != null)
        {
            _bikes.Remove(existingBike);
            _bikes.Add(bike);
        }
    }

    public void DeleteBike(int id) => _bikes.RemoveAll(b => b.Id == id);

    // BikeModel methods
    public List<BikeModel> GetAllModels() => [.. _models];

    public BikeModel? GetModelById(int id) => _models.FirstOrDefault(m => m.Id == id);

    public void AddModel(BikeModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        _models.Add(model);
    }

    public void UpdateModel(BikeModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        var existingModel = _models.FirstOrDefault(m => m.Id == model.Id);
        if (existingModel != null)
        {
            _models.Remove(existingModel);
            _models.Add(model);
        }
    }

    public void DeleteModel(int id) => _models.RemoveAll(m => m.Id == id);

    // Renter methods
    public List<Renter> GetAllRenters() => [.. _renters];

    public Renter? GetRenterById(int id) => _renters.FirstOrDefault(r => r.Id == id);

    public void AddRenter(Renter renter)
    {
        ArgumentNullException.ThrowIfNull(renter);
        _renters.Add(renter);
    }

    public void UpdateRenter(Renter renter)
    {
        ArgumentNullException.ThrowIfNull(renter);
        var existingRenter = _renters.FirstOrDefault(r => r.Id == renter.Id);
        if (existingRenter != null)
        {
            _renters.Remove(existingRenter);
            _renters.Add(renter);
        }
    }

    public void DeleteRenter(int id) => _renters.RemoveAll(r => r.Id == id);

    // Rent methods
    public List<Rent> GetAllRents() => [.. _rents];

    public Rent? GetRentById(int id) => _rents.FirstOrDefault(r => r.Id == id);

    public void AddRent(Rent rent)
    {
        ArgumentNullException.ThrowIfNull(rent);
        _rents.Add(rent);
    }

    public void UpdateRent(Rent rent)
    {
        ArgumentNullException.ThrowIfNull(rent);
        var existingRent = _rents.FirstOrDefault(r => r.Id == rent.Id);
        if (existingRent != null)
        {
            _rents.Remove(existingRent);
            _rents.Add(rent);
        }
    }

    public void DeleteRent(int id) => _rents.RemoveAll(r => r.Id == id);
}