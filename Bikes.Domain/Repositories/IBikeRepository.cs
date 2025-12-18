using Bikes.Domain.Models;

namespace Bikes.Domain.Repositories;

/// <summary>
/// Repository for bike data access
/// </summary>
public interface IBikeRepository
{
    public List<Bike> GetAllBikes();
    public Bike? GetBikeById(int id);
    public void AddBike(Bike bike);
    public void UpdateBike(Bike bike);
    public void DeleteBike(int id);
}