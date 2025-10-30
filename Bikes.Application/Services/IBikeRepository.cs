using Bikes.Domain.Models;

namespace Bikes.Application.Services;

/// <summary>
/// Repository for bike data access
/// </summary>
public interface IBikeRepository
{
    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<Bike> GetAllBikes();

    /// <summary>
    /// Get bike by identifier
    /// </summary>
    public Bike? GetBikeById(int id);

    /// <summary>
    /// Add new bike
    /// </summary>
    public void AddBike(Bike bike);

    /// <summary>
    /// Update bike
    /// </summary>
    public void UpdateBike(Bike bike);

    /// <summary>
    /// Delete bike by identifier
    /// </summary>
    public void DeleteBike(int id);

    /// <summary>
    /// Get all bike models
    /// </summary>
    public List<BikeModel> GetAllModels();

    /// <summary>
    /// Get all rental records
    /// </summary>
    public List<Rent> GetAllRents();

    /// <summary>
    /// Get all renters
    /// </summary>
    public List<Renter> GetAllRenters();
}