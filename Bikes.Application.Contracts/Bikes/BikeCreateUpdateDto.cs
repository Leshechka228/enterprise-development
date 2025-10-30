namespace Bikes.Application.Contracts.Bikes;

/// <summary>
/// DTO for creating and updating bikes
/// </summary>
public class BikeCreateUpdateDto
{
    /// <summary>
    /// Bike serial number
    /// </summary>
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Bike model identifier
    /// </summary>
    public required int ModelId { get; set; }

    /// <summary>
    /// Bike color
    /// </summary>
    public required string Color { get; set; }
}