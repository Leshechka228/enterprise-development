using Bikes.Application.Contracts;

namespace Bikes.Application.Contracts.Rents;

/// <summary>
/// Service for rent management
/// </summary>
public interface IRentService : IApplicationService
{
    /// <summary>
    /// Get all rents
    /// </summary>
    public List<RentDto> GetAllRents();

    /// <summary>
    /// Get rent by identifier
    /// </summary>
    public RentDto? GetRentById(int id);

    /// <summary>
    /// Create new rent
    /// </summary>
    public RentDto CreateRent(RentCreateUpdateDto request);

    /// <summary>
    /// Update rent
    /// </summary>
    public RentDto? UpdateRent(int id, RentCreateUpdateDto request);

    /// <summary>
    /// Delete rent
    /// </summary>
    public bool DeleteRent(int id);
}