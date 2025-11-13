using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Bikes;

/// <summary>
/// DTO for creating and updating bikes
/// </summary>
public class BikeCreateUpdateDto
{
    /// <summary>
    /// Bike serial number
    /// </summary>
    [Required(ErrorMessage = "Serial number is required")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "Serial number must be between 3 and 50 characters")]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Bike model identifier
    /// </summary>
    [Required(ErrorMessage = "Model ID is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Model ID must be a positive number")]
    public required int ModelId { get; set; }

    /// <summary>
    /// Bike color
    /// </summary>
    [Required(ErrorMessage = "Color is required")]
    [StringLength(30, MinimumLength = 2, ErrorMessage = "Color must be between 2 and 30 characters")]
    public required string Color { get; set; }
}