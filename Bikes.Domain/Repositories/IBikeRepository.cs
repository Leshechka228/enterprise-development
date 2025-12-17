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

/// <summary>
/// Repository for bike model data access
/// </summary>
public interface IBikeModelRepository
{
    public List<BikeModel> GetAllModels();
    public BikeModel? GetModelById(int id);
    public void AddModel(BikeModel model);
    public void UpdateModel(BikeModel model);
    public void DeleteModel(int id);
}

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