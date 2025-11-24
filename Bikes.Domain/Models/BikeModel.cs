using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bikes.Domain.Models;

/// <summary>
/// Bicycle model information
/// </summary>
[Table("bike_models")]
public class BikeModel
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Model name
    /// </summary>
    [Required]
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Type of bicycle
    /// </summary>
    [Required]
    [Column("type")]
    public required BikeType Type { get; set; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    [Required]
    [Column("wheel_size")]
    public required decimal WheelSize { get; set; }

    /// <summary>
    /// Maximum permissible passenger weight in kg
    /// </summary>
    [Required]
    [Column("max_weight")]
    public required decimal MaxWeight { get; set; }

    /// <summary>
    /// Bicycle weight in kg
    /// </summary>
    [Required]
    [Column("weight")]
    public required decimal Weight { get; set; }

    /// <summary>
    /// Type of brakes
    /// </summary>
    [Required]
    [Column("brake_type")]
    public required string BrakeType { get; set; }

    /// <summary>
    /// Model year
    /// </summary>
    [Required]
    [Column("model_year")]
    public required int ModelYear { get; set; }

    /// <summary>
    /// Price per hour of rental
    /// </summary>
    [Required]
    [Column("price_per_hour")]
    public required decimal PricePerHour { get; set; }

    /// <summary>
    /// Navigation property for bikes
    /// </summary>
    public virtual List<Bike> Bikes { get; set; } = new();
}