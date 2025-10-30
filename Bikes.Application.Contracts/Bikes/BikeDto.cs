namespace Bikes.Application.Contracts.Bikes;

/// <summary>
/// DTO for bike representation
/// </summary>
public class BikeDto
{
    /// <summary>
    /// Bike identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Bike serial number
    /// </summary>
    public string SerialNumber { get; set; } = string.Empty;

    /// <summary>
    /// Bike model identifier
    /// </summary>
    public int ModelId { get; set; }

    /// <summary>
    /// Bike color
    /// </summary>
    public string Color { get; set; } = string.Empty;

    /// <summary>
    /// Availability status for rental
    /// </summary>
    public bool IsAvailable { get; set; }
}