namespace Bikes.Application.Contracts.Models;

/// <summary>
/// Service for bike model management
/// </summary>
public interface IBikeModelService : IApplicationService<BikeModelDto, BikeModelCreateUpdateDto>
{
}