using Bikes.Application.Contracts.Analytics;
using Bikes.Application.Contracts.Bikes;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for rental analytics
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    private readonly IAnalyticsService _analyticsService = analyticsService;

    /// <summary>
    /// Get all sport bikes
    /// </summary>
    [HttpGet("sport-bikes")]
    public ActionResult<List<BikeDto>> GetSportBikes()
    {
        var sportBikes = _analyticsService.GetSportBikes();
        return Ok(sportBikes);
    }

    /// <summary>
    /// Get top 5 models by profit
    /// </summary>
    [HttpGet("top-models-by-profit")]
    public ActionResult<List<BikeModelAnalyticsDto>> GetTopModelsByProfit()
    {
        var topModels = _analyticsService.GetTop5ModelsByProfit();
        return Ok(topModels);
    }

    /// <summary>
    /// Get top 5 models by rental duration
    /// </summary>
    [HttpGet("top-models-by-duration")]
    public ActionResult<List<BikeModelAnalyticsDto>> GetTopModelsByDuration()
    {
        var topModels = _analyticsService.GetTop5ModelsByRentalDuration();
        return Ok(topModels);
    }

    /// <summary>
    /// Get rental statistics
    /// </summary>
    [HttpGet("rental-statistics")]
    public ActionResult<RentalStatistics> GetRentalStatistics()
    {
        var statistics = _analyticsService.GetRentalStatistics();
        return Ok(statistics);
    }

    /// <summary>
    /// Get total rental time by bike type
    /// </summary>
    [HttpGet("rental-time-by-type")]
    public ActionResult<Dictionary<string, int>> GetRentalTimeByType()
    {
        var rentalTime = _analyticsService.GetTotalRentalTimeByBikeType();
        return Ok(rentalTime);
    }

    /// <summary>
    /// Get top renters by rental count
    /// </summary>
    [HttpGet("top-renters")]
    public ActionResult<List<RenterAnalyticsDto>> GetTopRenters()
    {
        var topRenters = _analyticsService.GetTopRentersByRentalCount();
        return Ok(topRenters);
    }
}