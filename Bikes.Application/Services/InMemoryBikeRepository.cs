using Bikes.Domain.Models;
using Bikes.Tests;

namespace Bikes.Application.Services;

/// <summary>
/// In-memory implementation of bike repository
/// </summary>
public class InMemoryBikeRepository() : IBikeRepository
{
    private readonly BikesFixture _fixture = new();

    private readonly List<Bike> _bikes = [.. new BikesFixture().Bikes];
    private readonly List<BikeModel> _models = [.. new BikesFixture().Models];
    private readonly List<Rent> _rents = [.. new BikesFixture().Rents];
    private readonly List<Renter> _renters = [.. new BikesFixture().Renters];

    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<Bike> GetAllBikes() => [.. _bikes];

    /// <summary>
    /// Get bike by identifier
    /// </summary>
    public Bike? GetBikeById(int id) => _bikes.FirstOrDefault(b => b.Id == id);

    /// <summary>
    /// Add new bike
    /// </summary>
    public void AddBike(Bike bike)
    {
        if (bike == null)
            throw new ArgumentNullException(nameof(bike));

        _bikes.Add(bike);
    }

    /// <summary>
    /// Update bike
    /// </summary>
    public void UpdateBike(Bike bike)
    {
        if (bike == null)
            throw new ArgumentNullException(nameof(bike));

        var existingBike = _bikes.FirstOrDefault(b => b.Id == bike.Id);
        if (existingBike != null)
        {
            _bikes.Remove(existingBike);
            _bikes.Add(bike);
        }
    }

    /// <summary>
    /// Delete bike by identifier
    /// </summary>
    public void DeleteBike(int id) => _bikes.RemoveAll(b => b.Id == id);

    /// <summary>
    /// Get all bike models
    /// </summary>
    public List<BikeModel> GetAllModels() => [.. _models];

    /// <summary>
    /// Get all rental records
    /// </summary>
    public List<Rent> GetAllRents() => [.. _rents];

    /// <summary>
    /// Get all renters
    /// </summary>
    public List<Renter> GetAllRenters() => [.. _renters];
}