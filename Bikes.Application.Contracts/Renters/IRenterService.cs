namespace Bikes.Application.Contracts.Renters;

/// <summary>
/// Service for renter management
/// </summary>
public interface IRenterService : IApplicationService<RenterDto, RenterCreateUpdateDto>
{
}