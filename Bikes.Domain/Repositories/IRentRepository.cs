using Bikes.Domain.Models;

namespace Bikes.Domain.Repositories;

/// <summary>
/// Repository for rent data access
/// </summary>
public interface IRentRepository
{
    public List<Rent> GetAllRents();
    public Rent? GetRentById(int id);
    public void AddRent(Rent rent);
    public void UpdateRent(Rent rent);
    public void DeleteRent(int id);
}