namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle entity
/// </summary>
public class Bike
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Serial number of the bicycle
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Reference to bike model
    /// </summary>
    public required BikeModel Model { get; set; }

    /// <summary>
    /// Color of the bicycle
    /// </summary>
    public required string Color { get; set; }

    /// <summary>
    /// Availability status for rental
    /// </summary>
    public bool IsAvailable { get; set; } = true;
}