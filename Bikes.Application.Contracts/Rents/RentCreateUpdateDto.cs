namespace Bikes.Application.Contracts.Rents;

/// <summary>
/// DTO for creating and updating rents
/// </summary>
public class RentCreateUpdateDto
{
    /// <summary>
    /// Bike identifier
    /// </summary>
    public required int BikeId { get; set; }

    /// <summary>
    /// Renter identifier
    /// </summary>
    public required int RenterId { get; set; }

    /// <summary>
    /// Rental start time
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    public required int DurationHours { get; set; }
}