namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle renter information
/// </summary>
public class Renter
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Full name of the renter
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number
    /// </summary>
    public required string Phone { get; set; }
}