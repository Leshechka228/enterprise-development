namespace Bikes.Application.Contracts.Models;

/// <summary>
/// DTO for bike model representation
/// </summary>
public class BikeModelDto
{
    /// <summary>
    /// Model identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Model name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Bike type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    public decimal WheelSize { get; set; }

    /// <summary>
    /// Maximum weight capacity in kg
    /// </summary>
    public decimal MaxWeight { get; set; }

    /// <summary>
    /// Bike weight in kg
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Brake type
    /// </summary>
    public string BrakeType { get; set; } = string.Empty;

    /// <summary>
    /// Model year
    /// </summary>
    public int ModelYear { get; set; }

    /// <summary>
    /// Price per rental hour
    /// </summary>
    public decimal PricePerHour { get; set; }
}