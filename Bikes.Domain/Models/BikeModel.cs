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
    public string Name { get; set; } = null!;

    /// <summary>
    /// Type of bicycle
    /// </summary>
    [Required]
    [Column("type")]
    public BikeType Type { get; set; }

    /// <summary>
    /// Wheel size in inches
    /// </summary>
    [Required]
    [Column("wheel_size")]
    public decimal WheelSize { get; set; }

    /// <summary>
    /// Maximum permissible passenger weight in kg
    /// </summary>
    [Required]
    [Column("max_weight")]
    public decimal MaxWeight { get; set; }

    /// <summary>
    /// Bicycle weight in kg
    /// </summary>
    [Required]
    [Column("weight")]
    public decimal Weight { get; set; }

    /// <summary>
    /// Type of brakes
    /// </summary>
    [Required]
    [Column("brake_type")]
    public string BrakeType { get; set; } = null!;

    /// <summary>
    /// Model year
    /// </summary>
    [Required]
    [Column("model_year")]
    public int ModelYear { get; set; }

    /// <summary>
    /// Price per hour of rental
    /// </summary>
    [Required]
    [Column("price_per_hour")]
    public decimal PricePerHour { get; set; }

    /// <summary>
    /// Navigation property for bikes
    /// </summary>
    public virtual List<Bike> Bikes { get; set; } = [];
}