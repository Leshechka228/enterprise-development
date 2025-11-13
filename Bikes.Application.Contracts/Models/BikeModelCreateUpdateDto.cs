using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Models;

/// <summary>
/// DTO for creating and updating bike models
/// </summary>
public class BikeModelCreateUpdateDto
{
    /// <summary>
    /// Model name
    /// </summary>
    [Required(ErrorMessage = "Model name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Model name must be between 2 and 100 characters")]
    public required string Name { get; set; }

    /// <summary>
    /// Bike type
    /// </summary>
    [Required(ErrorMessage = "Bike type is required")]
    [RegularExpression("^(Mountain|Road|Hybrid|City|Sport)$", ErrorMessage = "Bike type must be Mountain, Road, Hybrid, City, or Sport")]
    public required string Type { get; set; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    [Required(ErrorMessage = "Wheel size is required")]
    [Range(10, 30, ErrorMessage = "Wheel size must be between 10 and 30 inches")]
    public required decimal WheelSize { get; set; }

    /// <summary>
    /// Maximum weight capacity in kg
    /// </summary>
    [Required(ErrorMessage = "Maximum weight capacity is required")]
    [Range(50, 300, ErrorMessage = "Maximum weight capacity must be between 50 and 300 kg")]
    public required decimal MaxWeight { get; set; }

    /// <summary>
    /// Bike weight in kg
    /// </summary>
    [Required(ErrorMessage = "Bike weight is required")]
    [Range(5, 50, ErrorMessage = "Bike weight must be between 5 and 50 kg")]
    public required decimal Weight { get; set; }

    /// <summary>
    /// Brake type
    /// </summary>
    [Required(ErrorMessage = "Brake type is required")]
    [RegularExpression("^(Mechanical|Hydraulic|Rim)$", ErrorMessage = "Brake type must be Mechanical, Hydraulic, or Rim")]
    public required string BrakeType { get; set; }

    /// <summary>
    /// Model year
    /// </summary>
    [Required(ErrorMessage = "Model year is required")]
    [Range(2000, 2030, ErrorMessage = "Model year must be between 2000 and 2030")]
    public required int ModelYear { get; set; }

    /// <summary>
    /// Price per rental hour
    /// </summary>
    [Required(ErrorMessage = "Price per hour is required")]
    [Range(0.01, 1000, ErrorMessage = "Price per hour must be between 0.01 and 1000")]
    public required decimal PricePerHour { get; set; }
}