using Bikes.Application.Contracts.Analytics;
using Bikes.Application.Contracts.Bikes;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of analytics service
/// </summary>
public class AnalyticsService(IBikeRepository repository) : IAnalyticsService
{
    /// <summary>
    /// Get all sport bikes
    /// </summary>
    public List<BikeDto> GetSportBikes()
    {
        return [.. repository.GetAllBikes()
            .Where(b => b.Model.Type == BikeType.Sport)
            .Select(b => new BikeDto
            {
                Id = b.Id,
                SerialNumber = b.SerialNumber,
                ModelId = b.Model.Id,
                Color = b.Color,
                IsAvailable = b.IsAvailable
            })];
    }

    /// <summary>
    /// Get top 5 models by profit
    /// </summary>
    public List<BikeModelAnalyticsDto> GetTop5ModelsByProfit()
    {
        var rents = repository.GetAllRents();

        return [.. rents
            .GroupBy(r => r.Bike.Model)
            .Select(g => new BikeModelAnalyticsDto
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Type = g.Key.Type.ToString(),
                PricePerHour = g.Key.PricePerHour,
                TotalProfit = g.Sum(r => r.TotalCost),
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalProfit)
            .Take(5)];
    }

    /// <summary>
    /// Get top 5 models by rental duration
    /// </summary>
    public List<BikeModelAnalyticsDto> GetTop5ModelsByRentalDuration()
    {
        var rents = repository.GetAllRents();

        return [.. rents
            .GroupBy(r => r.Bike.Model)
            .Select(g => new BikeModelAnalyticsDto
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Type = g.Key.Type.ToString(),
                PricePerHour = g.Key.PricePerHour,
                TotalProfit = g.Sum(r => r.TotalCost),
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Take(5)];
    }

    /// <summary>
    /// Get rental statistics
    /// </summary>
    public RentalStatistics GetRentalStatistics()
    {
        var durations = repository.GetAllRents()
            .Select(r => r.DurationHours);

        return new RentalStatistics(
            MinDuration: durations.Min(),
            MaxDuration: durations.Max(),
            AvgDuration: durations.Average()
        );
    }

    /// <summary>
    /// Get total rental time by bike type
    /// </summary>
    public Dictionary<string, int> GetTotalRentalTimeByBikeType()
    {
        return repository.GetAllRents()
            .GroupBy(r => r.Bike.Model.Type)
            .ToDictionary(
                g => g.Key.ToString(),
                g => g.Sum(r => r.DurationHours)
            );
    }

    /// <summary>
    /// Get top renters by rental count
    /// </summary>
    public List<RenterAnalyticsDto> GetTopRentersByRentalCount()
    {
        return [.. repository.GetAllRents()
            .GroupBy(r => r.Renter)
            .Select(g => new RenterAnalyticsDto
            {
                FullName = g.Key.FullName,
                RentalCount = g.Count()
            })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)];
    }
}