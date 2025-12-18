using Microsoft.EntityFrameworkCore;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Infrastructure.EfCore;

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