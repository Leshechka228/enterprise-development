namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle model information
/// </summary>
public class BikeModel
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Model name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Type of bicycle
    /// </summary>
    public required BikeType Type { get; set; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    public required decimal WheelSize { get; set; }

    /// <summary>
    /// Maximum permissible passenger weight in kg
    /// </summary>
    public required decimal MaxWeight { get; set; }

    /// <summary>
    /// Bicycle weight in kg
    /// </summary>
    public required decimal Weight { get; set; }

    /// <summary>
    /// Type of brakes
    /// </summary>
    public required string BrakeType { get; set; }

    /// <summary>
    /// Model year
    /// </summary>
    public required int ModelYear { get; set; }

    /// <summary>
    /// Price per hour of rental
    /// </summary>
    public required decimal PricePerHour { get; set; }
}