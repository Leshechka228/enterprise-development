using Bikes.Application.Contracts;

namespace Bikes.Application.Contracts.Models;

/// <summary>
/// Service for bike model management
/// </summary>
public interface IBikeModelService : IApplicationService
{
    /// <summary>
    /// Get all bike models
    /// </summary>
    public List<BikeModelDto> GetAllModels();

    /// <summary>
    /// Get bike model by identifier
    /// </summary>
    public BikeModelDto? GetModelById(int id);

    /// <summary>
    /// Create new bike model
    /// </summary>
    public BikeModelDto CreateModel(BikeModelCreateUpdateDto request);

    /// <summary>
    /// Update bike model
    /// </summary>
    public BikeModelDto? UpdateModel(int id, BikeModelCreateUpdateDto request);

    /// <summary>
    /// Delete bike model
    /// </summary>
    public bool DeleteModel(int id);
}