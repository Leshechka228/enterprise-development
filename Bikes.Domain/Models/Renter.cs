using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle renter information
/// </summary>
[Table("renters")]
public class Renter
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Full name of the renter
    /// </summary>
    [Required]
    [Column("full_name")]
    public required string FullName { get; set; }

    /// <summary>
    /// Contact phone number
    /// </summary>
    [Required]
    [Column("phone")]
    public required string Phone { get; set; }

    /// <summary>
    /// Navigation property for rents
    /// </summary>
    public virtual List<Rent> Rents { get; set; } = new();
}