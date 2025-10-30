namespace Bikes.Application.Contracts.Renters;

/// <summary>
/// DTO for renter representation
/// </summary>
public class RenterDto
{
    /// <summary>
    /// Renter identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Renter full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Contact phone number
    /// </summary>
    public string Phone { get; set; } = string.Empty;
}