using Bikes.Application.Contracts;

namespace Bikes.Application.Contracts.Renters;

/// <summary>
/// Service for renter management
/// </summary>
public interface IRenterService : IApplicationService
{
    /// <summary>
    /// Get all renters
    /// </summary>
    public List<RenterDto> GetAllRenters();

    /// <summary>
    /// Get renter by identifier
    /// </summary>
    public RenterDto? GetRenterById(int id);

    /// <summary>
    /// Create new renter
    /// </summary>
    public RenterDto CreateRenter(RenterCreateUpdateDto request);

    /// <summary>
    /// Update renter
    /// </summary>
    public RenterDto? UpdateRenter(int id, RenterCreateUpdateDto request);

    /// <summary>
    /// Delete renter
    /// </summary>
    public bool DeleteRenter(int id);
}