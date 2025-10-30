using Bikes.Application.Contracts.Bikes;
using Bikes.Domain.Models;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of bike service
/// </summary>
public class BikeService(IBikeRepository repository) : IBikeService
{
    private readonly IBikeRepository _repository = repository;

    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<BikeDto> GetAllBikes()
    {
        return _repository.GetAllBikes().Select(b => new BikeDto
        {
            Id = b.Id,
            SerialNumber = b.SerialNumber,
            ModelId = b.Model.Id,
            Color = b.Color,
            IsAvailable = b.IsAvailable
        }).ToList();
    }

    /// <summary>
    /// Get bike by identifier
    /// </summary>
    public BikeDto? GetBikeById(int id)
    {
        var bike = _repository.GetBikeById(id);
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
    public BikeDto CreateBike(BikeCreateUpdateDto request)
    {
        var models = _repository.GetAllModels();
        var model = models.FirstOrDefault(m => m.Id == request.ModelId);
        if (model == null)
            throw new InvalidOperationException("Model not found");

        var newBike = new Bike
        {
            Id = _repository.GetAllBikes().Max(b => b.Id) + 1,
            SerialNumber = request.SerialNumber,
            Model = model,
            Color = request.Color,
            IsAvailable = true
        };

        _repository.AddBike(newBike);

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
    public BikeDto? UpdateBike(int id, BikeCreateUpdateDto request)
    {
        var bike = _repository.GetBikeById(id);
        if (bike == null) return null;

        var models = _repository.GetAllModels();
        var model = models.FirstOrDefault(m => m.Id == request.ModelId);
        if (model == null)
            throw new InvalidOperationException("Model not found");

        bike.SerialNumber = request.SerialNumber;
        bike.Model = model;
        bike.Color = request.Color;

        _repository.UpdateBike(bike);

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
    public bool DeleteBike(int id)
    {
        var bike = _repository.GetBikeById(id);
        if (bike == null) return false;

        _repository.DeleteBike(id);
        return true;
    }
}