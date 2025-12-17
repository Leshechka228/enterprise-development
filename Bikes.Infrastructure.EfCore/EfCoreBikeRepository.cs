using Microsoft.EntityFrameworkCore;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Infrastructure.EfCore;

/// <summary>
/// EF Core implementation of bike repository
/// </summary>
public class EfCoreBikeRepository(BikesDbContext context) : IBikeRepository
{
    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<Bike> GetAllBikes()
    {
        return [.. context.Bikes
            .Include(b => b.Model)
            .AsNoTracking()];
    }

    /// <summary>
    /// Get bike by identifier
    /// </summary>
    public Bike? GetBikeById(int id)
    {
        return context.Bikes
            .Include(b => b.Model)
            .AsNoTracking()
            .FirstOrDefault(b => b.Id == id);
    }

    /// <summary>
    /// Add new bike
    /// </summary>
    public void AddBike(Bike bike)
    {
        ArgumentNullException.ThrowIfNull(bike);
        context.Bikes.Add(bike);
        context.SaveChanges();
    }

    /// <summary>
    /// Update bike
    /// </summary>
    public void UpdateBike(Bike bike)
    {
        ArgumentNullException.ThrowIfNull(bike);
        context.Bikes.Update(bike);
        context.SaveChanges();
    }

    /// <summary>
    /// Delete bike
    /// </summary>
    public void DeleteBike(int id)
    {
        var bike = context.Bikes.Find(id);
        if (bike != null)
        {
            context.Bikes.Remove(bike);
            context.SaveChanges();
        }
    }
}

/// <summary>
/// EF Core implementation of bike model repository
/// </summary>
public class EfCoreBikeModelRepository(BikesDbContext context) : IBikeModelRepository
{
    /// <summary>
    /// Get all bike models
    /// </summary>
    public List<BikeModel> GetAllModels()
    {
        return [.. context.BikeModels.AsNoTracking()];
    }

    /// <summary>
    /// Get bike model by identifier
    /// </summary>
    public BikeModel? GetModelById(int id)
    {
        return context.BikeModels
            .AsNoTracking()
            .FirstOrDefault(m => m.Id == id);
    }

    /// <summary>
    /// Add new bike model
    /// </summary>
    public void AddModel(BikeModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        context.BikeModels.Add(model);
        context.SaveChanges();
    }

    /// <summary>
    /// Update bike model
    /// </summary>
    public void UpdateModel(BikeModel model)
    {
        ArgumentNullException.ThrowIfNull(model);
        context.BikeModels.Update(model);
        context.SaveChanges();
    }

    /// <summary>
    /// Delete bike model
    /// </summary>
    public void DeleteModel(int id)
    {
        var model = context.BikeModels.Find(id);
        if (model != null)
        {
            context.BikeModels.Remove(model);
            context.SaveChanges();
        }
    }
}

/// <summary>
/// EF Core implementation of renter repository
/// </summary>
public class EfCoreRenterRepository(BikesDbContext context) : IRenterRepository
{
    /// <summary>
    /// Get all renters
    /// </summary>
    public List<Renter> GetAllRenters()
    {
        return [.. context.Renters.AsNoTracking()];
    }

    /// <summary>
    /// Get renter by identifier
    /// </summary>
    public Renter? GetRenterById(int id)
    {
        return context.Renters
            .AsNoTracking()
            .FirstOrDefault(r => r.Id == id);
    }

    /// <summary>
    /// Add new renter
    /// </summary>
    public void AddRenter(Renter renter)
    {
        ArgumentNullException.ThrowIfNull(renter);
        context.Renters.Add(renter);
        context.SaveChanges();
    }

    /// <summary>
    /// Update renter
    /// </summary>
    public void UpdateRenter(Renter renter)
    {
        ArgumentNullException.ThrowIfNull(renter);
        context.Renters.Update(renter);
        context.SaveChanges();
    }

    /// <summary>
    /// Delete renter
    /// </summary>
    public void DeleteRenter(int id)
    {
        var renter = context.Renters.Find(id);
        if (renter != null)
        {
            context.Renters.Remove(renter);
            context.SaveChanges();
        }
    }
}

/// <summary>
/// EF Core implementation of rent repository
/// </summary>
public class EfCoreRentRepository(BikesDbContext context) : IRentRepository
{
    /// <summary>
    /// Get all rents
    /// </summary>
    public List<Rent> GetAllRents()
    {
        return [.. context.Rents
            .Include(r => r.Bike)
                .ThenInclude(b => b.Model)
            .Include(r => r.Renter)
            .AsNoTracking()];
    }

    /// <summary>
    /// Get rent by identifier
    /// </summary>
    public Rent? GetRentById(int id)
    {
        return context.Rents
            .Include(r => r.Bike)
                .ThenInclude(b => b.Model)
            .Include(r => r.Renter)
            .AsNoTracking()
            .FirstOrDefault(r => r.Id == id);
    }

    /// <summary>
    /// Add new rent
    /// </summary>
    public void AddRent(Rent rent)
    {
        ArgumentNullException.ThrowIfNull(rent);
        context.Rents.Add(rent);
        context.SaveChanges();
    }

    /// <summary>
    /// Update rent
    /// </summary>
    public void UpdateRent(Rent rent)
    {
        ArgumentNullException.ThrowIfNull(rent);
        context.Rents.Update(rent);
        context.SaveChanges();
    }

    /// <summary>
    /// Delete rent
    /// </summary>
    public void DeleteRent(int id)
    {
        var rent = context.Rents.Find(id);
        if (rent != null)
        {
            context.Rents.Remove(rent);
            context.SaveChanges();
        }
    }
}