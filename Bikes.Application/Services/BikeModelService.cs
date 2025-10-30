using Bikes.Application.Contracts.Models;
using Bikes.Domain.Models;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of bike model service
/// </summary>
public class BikeModelService(IBikeRepository repository) : IBikeModelService
{
    private readonly IBikeRepository _repository = repository;

    /// <summary>
    /// Get all bike models
    /// </summary>
    public List<BikeModelDto> GetAllModels()
    {
        return _repository.GetAllModels().Select(m => new BikeModelDto
        {
            Id = m.Id,
            Name = m.Name,
            Type = m.Type.ToString(),
            WheelSize = m.WheelSize,
            MaxWeight = m.MaxWeight,
            Weight = m.Weight,
            BrakeType = m.BrakeType,
            ModelYear = m.ModelYear,
            PricePerHour = m.PricePerHour
        }).ToList();
    }

    /// <summary>
    /// Get bike model by identifier
    /// </summary>
    public BikeModelDto? GetModelById(int id)
    {
        var model = _repository.GetAllModels().FirstOrDefault(m => m.Id == id);
        return model == null ? null : new BikeModelDto
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type.ToString(),
            WheelSize = model.WheelSize,
            MaxWeight = model.MaxWeight,
            Weight = model.Weight,
            BrakeType = model.BrakeType,
            ModelYear = model.ModelYear,
            PricePerHour = model.PricePerHour
        };
    }

    /// <summary>
    /// Create new bike model
    /// </summary>
    public BikeModelDto CreateModel(BikeModelCreateUpdateDto request)
    {
        if (!Enum.TryParse<BikeType>(request.Type, out var bikeType))
            throw new InvalidOperationException($"Invalid bike type: {request.Type}");

        var models = _repository.GetAllModels();
        var newModel = new BikeModel
        {
            Id = models.Max(m => m.Id) + 1,
            Name = request.Name,
            Type = bikeType,
            WheelSize = request.WheelSize,
            MaxWeight = request.MaxWeight,
            Weight = request.Weight,
            BrakeType = request.BrakeType,
            ModelYear = request.ModelYear,
            PricePerHour = request.PricePerHour
        };

        return new BikeModelDto
        {
            Id = newModel.Id,
            Name = newModel.Name,
            Type = newModel.Type.ToString(),
            WheelSize = newModel.WheelSize,
            MaxWeight = newModel.MaxWeight,
            Weight = newModel.Weight,
            BrakeType = newModel.BrakeType,
            ModelYear = newModel.ModelYear,
            PricePerHour = newModel.PricePerHour
        };
    }

    /// <summary>
    /// Update bike model
    /// </summary>
    public BikeModelDto? UpdateModel(int id, BikeModelCreateUpdateDto request)
    {
        if (!Enum.TryParse<BikeType>(request.Type, out var bikeType))
            throw new InvalidOperationException($"Invalid bike type: {request.Type}");

        var models = _repository.GetAllModels();
        var model = models.FirstOrDefault(m => m.Id == id);
        if (model == null) return null;

        model.Name = request.Name;
        model.Type = bikeType;
        model.WheelSize = request.WheelSize;
        model.MaxWeight = request.MaxWeight;
        model.Weight = request.Weight;
        model.BrakeType = request.BrakeType;
        model.ModelYear = request.ModelYear;
        model.PricePerHour = request.PricePerHour;

        return new BikeModelDto
        {
            Id = model.Id,
            Name = model.Name,
            Type = model.Type.ToString(),
            WheelSize = model.WheelSize,
            MaxWeight = model.MaxWeight,
            Weight = model.Weight,
            BrakeType = model.BrakeType,
            ModelYear = model.ModelYear,
            PricePerHour = model.PricePerHour
        };
    }

    /// <summary>
    /// Delete bike model
    /// </summary>
    public bool DeleteModel(int id)
    {
        var models = _repository.GetAllModels();
        var model = models.FirstOrDefault(m => m.Id == id);
        return model != null;
    }
}