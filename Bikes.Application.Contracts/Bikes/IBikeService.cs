using Bikes.Application.Contracts;

namespace Bikes.Application.Contracts.Bikes;

/// <summary>
/// Service for bike management
/// </summary>
public interface IBikeService : IApplicationService
{
    /// <summary>
    /// Get all bikes
    /// </summary>
    public List<BikeDto> GetAllBikes();

    /// <summary>
    /// Get bike by identifier
    /// </summary>
    public BikeDto? GetBikeById(int id);

    /// <summary>
    /// Create new bike
    /// </summary>
    public BikeDto CreateBike(BikeCreateUpdateDto request);

    /// <summary>
    /// Update bike
    /// </summary>
    public BikeDto? UpdateBike(int id, BikeCreateUpdateDto request);

    /// <summary>
    /// Delete bike
    /// </summary>
    public bool DeleteBike(int id);
}