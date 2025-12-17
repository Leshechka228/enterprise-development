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
    public string FullName { get; set; } = null!;

    /// <summary>
    /// Contact phone number
    /// </summary>
    [Required]
    [Column("phone")]
    public string Phone { get; set; } = null!;

    /// <summary>
    /// Navigation property for rents
    /// </summary>
    public virtual List<Rent> Rents { get; set; } = [];
}