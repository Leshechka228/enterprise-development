namespace Bikes.Application.Contracts.Renters;

/// <summary>
/// DTO for creating and updating renters
/// </summary>
public class RenterCreateUpdateDto
{
    /// <summary>
    /// Renter full name
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number
    /// </summary>
    public required string Phone { get; set; }
}