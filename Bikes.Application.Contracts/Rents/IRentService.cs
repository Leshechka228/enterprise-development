namespace Bikes.Application.Contracts.Rents;

/// <summary>
/// Service for rent management
/// </summary>
public interface IRentService : IApplicationService<RentDto, RentCreateUpdateDto>
{
}