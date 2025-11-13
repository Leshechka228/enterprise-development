using System.ComponentModel.DataAnnotations;

namespace Bikes.Application.Contracts.Renters;

/// <summary>
/// DTO for creating and updating renters
/// </summary>
public class RenterCreateUpdateDto
{
    /// <summary>
    /// Renter full name
    /// </summary>
    [Required(ErrorMessage = "Full name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full name must be between 2 and 100 characters")]
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number
    /// </summary>
    [Required(ErrorMessage = "Phone number is required")]
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be in valid international format")]
    public required string Phone { get; set; }
}