namespace Bikes.Application.Contracts.Models;

/// <summary>
/// DTO for creating and updating bike models
/// </summary>
public class BikeModelCreateUpdateDto
{
    /// <summary>
    /// Model name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Bike type
    /// </summary>
    public required string Type { get; set; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    public required decimal WheelSize { get; set; }

    /// <summary>
    /// Maximum weight capacity in kg
    /// </summary>
    public required decimal MaxWeight { get; set; }

    /// <summary>
    /// Bike weight in kg
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Brake type
    /// </summary>
    public required string BrakeType { get; set; }

    /// <summary>
    /// Model year
    /// </summary>
    public required int ModelYear { get; set; }

    /// <summary>
    /// Price per rental hour
    /// </summary>
    public required decimal PricePerHour { get; set; }
}