namespace Bikes.Application.Contracts.Bikes;

/// <summary>
/// Service for bike management
/// </summary>
public interface IBikeService : IApplicationService<BikeDto, BikeCreateUpdateDto>
{
}