namespace Bikes.Application.Contracts.Rents;

/// <summary>
/// DTO for rent representation
/// </summary>
public class RentDto
{
    /// <summary>
    /// Rent identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Rented bike identifier
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

    /// <summary>
    /// Total rental cost
    /// </summary>
    public required decimal TotalCost { get; set; }
}