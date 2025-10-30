using Bikes.Application.Contracts;
using Bikes.Application.Contracts.Bikes;

namespace Bikes.Application.Contracts.Analytics;

/// <summary>
/// Service for bike rental analytics
/// </summary>
public interface IAnalyticsService : IApplicationService
{
    /// <summary>
    /// Get all sport bikes
    /// </summary>
    public List<BikeDto> GetSportBikes();

    /// <summary>
    /// Get top 5 models by profit
    /// </summary>
    public List<BikeModelAnalyticsDto> GetTop5ModelsByProfit();

    /// <summary>
    /// Get top 5 models by rental duration
    /// </summary>
    public List<BikeModelAnalyticsDto> GetTop5ModelsByRentalDuration();

    /// <summary>
    /// Get rental statistics
    /// </summary>
    public RentalStatistics GetRentalStatistics();

    /// <summary>
    /// Get total rental time by bike type
    /// </summary>
    public Dictionary<string, int> GetTotalRentalTimeByBikeType();

    /// <summary>
    /// Get top renters by rental count
    /// </summary>
    public List<RenterAnalyticsDto> GetTopRentersByRentalCount();
}