using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Rents;

/// <summary>
/// DTO for creating and updating rents
/// </summary>
public class RentCreateUpdateDto
{
    /// <summary>
    /// Bike identifier
    /// </summary>
    [Required(ErrorMessage = "Bike ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Bike ID must be a positive number")]
    public required int BikeId { get; set; }

    /// <summary>
    /// Renter identifier
    /// </summary>
    [Required(ErrorMessage = "Renter ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Renter ID must be a positive number")]
    public required int RenterId { get; set; }

    /// <summary>
    /// Rental start time
    /// </summary>
    [Required(ErrorMessage = "Start time is required")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    [Required(ErrorMessage = "Duration is required")]
    [Range(1, 24 * 7, ErrorMessage = "Duration must be between 1 and 168 hours (1 week)")]
    public required int DurationHours { get; set; }
}