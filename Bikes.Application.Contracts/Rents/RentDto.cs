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
    public int BikeId { get; set; }

    /// <summary>
    /// Renter identifier
    /// </summary>
    public int RenterId { get; set; }

    /// <summary>
    /// Rental start time
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    public int DurationHours { get; set; }

    /// <summary>
    /// Total rental cost
    /// </summary>
    public decimal TotalCost { get; set; }
}