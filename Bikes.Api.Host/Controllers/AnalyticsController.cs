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
    /// <summary>
    /// Get all sport bikes
    /// </summary>
    [HttpGet("sport-bikes")]
    public ActionResult<List<BikeDto>> GetSportBikes()
    {
        try
        {
            var sportBikes = analyticsService.GetSportBikes();
            return Ok(sportBikes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Get top 5 models by profit
    /// </summary>
    [HttpGet("top-models-by-profit")]
    public ActionResult<List<BikeModelAnalyticsDto>> GetTopModelsByProfit()
    {
        try
        {
            var topModels = analyticsService.GetTop5ModelsByProfit();
            return Ok(topModels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Get top 5 models by rental duration
    /// </summary>
    [HttpGet("top-models-by-duration")]
    public ActionResult<List<BikeModelAnalyticsDto>> GetTopModelsByDuration()
    {
        try
        {
            var topModels = analyticsService.GetTop5ModelsByRentalDuration();
            return Ok(topModels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Get rental statistics
    /// </summary>
    [HttpGet("rental-statistics")]
    public ActionResult<RentalStatistics> GetRentalStatistics()
    {
        try
        {
            var statistics = analyticsService.GetRentalStatistics();
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Get total rental time by bike type
    /// </summary>
    [HttpGet("rental-time-by-type")]
    public ActionResult<Dictionary<string, int>> GetRentalTimeByType()
    {
        try
        {
            var rentalTime = analyticsService.GetTotalRentalTimeByBikeType();
            return Ok(rentalTime);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Get top renters by rental count
    /// </summary>
    [HttpGet("top-renters")]
    public ActionResult<List<RenterAnalyticsDto>> GetTopRenters()
    {
        try
        {
            var topRenters = analyticsService.GetTopRentersByRentalCount();
            return Ok(topRenters);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}