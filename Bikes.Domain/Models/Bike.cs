using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle entity
/// </summary>
[Table("bikes")]
public class Bike
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Serial number of the bicycle
    /// </summary>
    [Required]
    [Column("serial_number")]
    public required string SerialNumber { get; set; }

    /// <summary>
    /// Foreign key for bike model
    /// </summary>
    [Required]
    [Column("model_id")]
    public int ModelId { get; set; }

    /// <summary>
    /// Reference to bike model
    /// </summary>
    public virtual BikeModel Model { get; set; } = null!;

    /// <summary>
    /// Color of the bicycle
    /// </summary>
    [Required]
    [Column("color")]
    public required string Color { get; set; }

    /// <summary>
    /// Availability status for rental
    /// </summary>
    [Required]
    [Column("is_available")]
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Navigation property for rents
    /// </summary>
    public virtual List<Rent> Rents { get; set; } = new();
}