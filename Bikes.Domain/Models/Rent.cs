using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle rental record
/// </summary>
[Table("rents")]
public class Rent
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Foreign key for bike
    /// </summary>
    [Required]
    [Column("bike_id")]
    public int BikeId { get; set; }

    /// <summary>
    /// Reference to rented bicycle
    /// </summary>
    public virtual Bike Bike { get; set; } = null!;

    /// <summary>
    /// Foreign key for renter
    /// </summary>
    [Required]
    [Column("renter_id")]
    public int RenterId { get; set; }

    /// <summary>
    /// Reference to renter
    /// </summary>
    public virtual Renter Renter { get; set; } = null!;

    /// <summary>
    /// Rental start time
    /// </summary>
    [Required]
    [Column("start_time")]
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// Rental duration in hours
    /// </summary>
    [Required]
    [Column("duration_hours")]
    public required int DurationHours { get; set; }

    /// <summary>
    /// Total rental cost
    /// </summary>
    [Column("total_cost")]
    public decimal TotalCost => Bike.Model.PricePerHour * DurationHours;
}