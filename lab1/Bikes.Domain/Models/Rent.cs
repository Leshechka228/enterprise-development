namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle rental record
/// </summary>
public class Rent
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Reference to rented bicycle
    /// </summary>
    public required Bike Bike { get; set; }

    /// <summary>
    /// Reference to renter
    /// </summary>
    public required Renter Renter { get; set; }

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
    public decimal TotalCost => Bike.Model.PricePerHour * DurationHours;
}