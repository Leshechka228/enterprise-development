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
// НИЧЕГО БОЛЬШЕ ТУТ НЕ ДОЛЖНО БЫТЬ!