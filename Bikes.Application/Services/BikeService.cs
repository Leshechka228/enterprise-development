using Bikes.Application.Contracts.Bikes;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of bike service
/// </summary>
public class BikeService(
    IBikeRepository bikeRepository,
    IBikeModelRepository bikeModelRepository) : IBikeService
{
    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<BikeDto> GetAll()
    {
        return [.. bikeRepository.GetAllBikes().Select(b => new BikeDto
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
        var bike = bikeRepository.GetBikeById(id);
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

        var modelExists = bikeModelRepository.GetModelById(request.ModelId) != null;
        if (!modelExists)
            throw new InvalidOperationException("Model not found");

        var newBike = new Bike
        {
            SerialNumber = request.SerialNumber,
            ModelId = request.ModelId,
            Color = request.Color,
            IsAvailable = true
        };

        bikeRepository.AddBike(newBike);

        return new BikeDto
        {
            Id = newBike.Id,
            SerialNumber = newBike.SerialNumber,
            ModelId = newBike.ModelId,
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

        var bike = bikeRepository.GetBikeById(id);
        if (bike == null) return null;

        var modelExists = bikeModelRepository.GetModelById(request.ModelId) != null;
        if (!modelExists)
            throw new InvalidOperationException("Model not found");

        bike.SerialNumber = request.SerialNumber;
        bike.ModelId = request.ModelId;
        bike.Color = request.Color;

        bikeRepository.UpdateBike(bike);

        return new BikeDto
        {
            Id = bike.Id,
            SerialNumber = bike.SerialNumber,
            ModelId = bike.ModelId,
            Color = bike.Color,
            IsAvailable = bike.IsAvailable
        };
    }

    /// <summary>
    /// Delete bike
    /// </summary>
    public bool Delete(int id)
    {
        var bike = bikeRepository.GetBikeById(id);
        if (bike == null) return false;

        bikeRepository.DeleteBike(id);
        return true;
    }
}