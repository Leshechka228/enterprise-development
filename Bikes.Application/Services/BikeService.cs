using Bikes.Application.Contracts.Bikes;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of bike service
/// </summary>
public class BikeService(IBikeRepository repository) : IBikeService
{
    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<BikeDto> GetAll()
    {
        return [.. repository.GetAllBikes().Select(b => new BikeDto
        {
            Id = b.Id,
            SerialNumber = b.SerialNumber,
            ModelId = b.Model.Id,
            Color = b.Color,
            IsAvailable = b.IsAvailable
        })];
    }

    /// <summary>
    /// Get bike by identifier
    /// </summary>
    public BikeDto? GetById(int id)
    {
        var bike = repository.GetBikeById(id);
        return bike == null ? null : new BikeDto
        {
            Id = bike.Id,
            SerialNumber = bike.SerialNumber,
            ModelId = bike.Model.Id,
            Color = bike.Color,
            IsAvailable = bike.IsAvailable
        };
    }

    /// <summary>
    /// Create new bike
    /// </summary>
    public BikeDto Create(BikeCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var models = repository.GetAllModels();
        var model = models.FirstOrDefault(m => m.Id == request.ModelId);
        if (model == null)
            throw new InvalidOperationException("Model not found");

        var newBike = new Bike
        {
            Id = repository.GetAllBikes().Max(b => b.Id) + 1,
            SerialNumber = request.SerialNumber,
            Model = model,
            Color = request.Color,
            IsAvailable = true
        };

        repository.AddBike(newBike);

        return new BikeDto
        {
            Id = newBike.Id,
            SerialNumber = newBike.SerialNumber,
            ModelId = newBike.Model.Id,
            Color = newBike.Color,
            IsAvailable = newBike.IsAvailable
        };
    }

    /// <summary>
    /// Update bike
    /// </summary>
    public BikeDto? Update(int id, BikeCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var bike = repository.GetBikeById(id);
        if (bike == null) return null;

        var models = repository.GetAllModels();
        var model = models.FirstOrDefault(m => m.Id == request.ModelId);
        if (model == null)
            throw new InvalidOperationException("Model not found");

        bike.SerialNumber = request.SerialNumber;
        bike.Model = model;
        bike.Color = request.Color;

        repository.UpdateBike(bike);

        return new BikeDto
        {
            Id = bike.Id,
            SerialNumber = bike.SerialNumber,
            ModelId = bike.Model.Id,
            Color = bike.Color,
            IsAvailable = bike.IsAvailable
        };
    }

    /// <summary>
    /// Delete bike
    /// </summary>
    public bool Delete(int id)
    {
        var bike = repository.GetBikeById(id);
        if (bike == null) return false;

        repository.DeleteBike(id);
        return true;
    }
}