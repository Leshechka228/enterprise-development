using Microsoft.EntityFrameworkCore;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Infrastructure.EfCore;

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