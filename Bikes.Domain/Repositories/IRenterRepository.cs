using Bikes.Domain.Models;

namespace Bikes.Domain.Repositories;

/// <summary>
/// Repository for renter data access
/// </summary>
public interface IRenterRepository
{
    public List<Renter> GetAllRenters();
    public Renter? GetRenterById(int id);
    public void AddRenter(Renter renter);
    public void UpdateRenter(Renter renter);
    public void DeleteRenter(int id);
}