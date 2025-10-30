namespace Bikes.Application.Contracts.Analytics;

/// <summary>
/// DTO for bike models analytics
/// </summary>
public class BikeModelAnalyticsDto
{
    /// <summary>
    /// Model identifier
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Model name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Bike type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Price per rental hour
    /// </summary>
    public decimal PricePerHour { get; set; }

    /// <summary>
    /// Total profit
    /// </summary>
    public decimal TotalProfit { get; set; }

    /// <summary>
    /// Total rental duration
    /// </summary>
    public int TotalDuration { get; set; }
}

/// <summary>
/// DTO for renters analytics
/// </summary>
public class RenterAnalyticsDto
{
    /// <summary>
    /// Renter full name
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Number of rentals
    /// </summary>
    public int RentalCount { get; set; }
}

/// <summary>
/// Rental statistics
/// </summary>
public record RentalStatistics(
    /// <summary>
    /// Minimum rental duration
    /// </summary>
    int MinDuration,

    /// <summary>
    /// Maximum rental duration
    /// </summary>
    int MaxDuration,

    /// <summary>
    /// Average rental duration
    /// </summary>
    double AvgDuration
);