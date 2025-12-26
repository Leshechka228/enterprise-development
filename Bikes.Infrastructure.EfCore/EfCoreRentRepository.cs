using Microsoft.EntityFrameworkCore;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Infrastructure.EfCore;

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

        if (rent.Bike != null && context.Entry(rent.Bike).State == EntityState.Detached)
        {
            context.Attach(rent.Bike);
            
            if (rent.Bike.Model != null && context.Entry(rent.Bike.Model).State == EntityState.Detached)
            {
                context.Attach(rent.Bike.Model);
            }
        }
        
        if (rent.Renter != null && context.Entry(rent.Renter).State == EntityState.Detached)
        {
            context.Attach(rent.Renter);
        }
        
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