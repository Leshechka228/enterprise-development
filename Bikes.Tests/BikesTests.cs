using Bikes.Domain.Models;

namespace Bikes.Tests;

public class BikesTests(BikesFixture fixture) : IClassFixture<BikesFixture>
{
    /// <summary>
    /// Test for retrieving all sport bikes information
    /// </summary>
    [Fact]
    public void GetAllSportBikes()
    {
        var sportBikes = fixture.Bikes
            .Where(b => b.Model.Type == BikeType.Sport)
            .ToList();

        var expectedSportBikeNames = new[]
        {
            "Sport Pro 1000", "Sport Elite", "Sport Lightning", "Sport Thunder"
        };
        var expectedCount = 6;

        Assert.NotNull(sportBikes);
        Assert.Equal(expectedCount, sportBikes.Count);
        Assert.All(sportBikes, bike => Assert.Equal(BikeType.Sport, bike.Model.Type));
        Assert.All(sportBikes, bike => Assert.Contains(bike.Model.Name, expectedSportBikeNames));
    }

    /// <summary>
    /// Test for retrieving top 5 bike models by rental profit
    /// </summary>
    [Fact]
    public void Top5ModelsByProfit()
    {
        var topModelsByProfit = fixture.Rents
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Model = g.Key,
                TotalProfit = g.Sum(r => r.TotalCost)
            })
            .OrderByDescending(x => x.TotalProfit)
            .Take(5)
            .ToList();

        var expectedCount = 5;
        var expectedTopModel = "Electric Mountain";

        Assert.NotNull(topModelsByProfit);
        Assert.True(topModelsByProfit.Count <= expectedCount);
        Assert.Equal(expectedCount, topModelsByProfit.Count);
        Assert.Equal(expectedTopModel, topModelsByProfit.First().Model.Name);
    }

    /// <summary>
    /// Test for retrieving top 5 bike models by rental duration
    /// </summary>
    [Fact]
    public void Top5ModelsByRentalDuration()
    {
        var topModelsByDuration = fixture.Rents
            .GroupBy(r => r.Bike.Model)
            .Select(g => new
            {
                Model = g.Key,
                TotalDuration = g.Sum(r => r.DurationHours)
            })
            .OrderByDescending(x => x.TotalDuration)
            .Take(5)
            .ToList();

        var expectedCount = 5;
        var expectedTopModel = "Sport Lightning";
        var expectedTopDuration = 19;

        Assert.NotNull(topModelsByDuration);
        Assert.True(topModelsByDuration.Count <= expectedCount);
        Assert.Equal(expectedCount, topModelsByDuration.Count);
        Assert.Equal(expectedTopModel, topModelsByDuration.First().Model.Name);
        Assert.Equal(expectedTopDuration, topModelsByDuration.First().TotalDuration);
    }

    /// <summary>
    /// Test for rental statistics - min, max and average rental duration
    /// </summary>
    [Fact]
    public void RentalStatistics()
    {
        var durations = fixture.Rents.Select(r => r.DurationHours).ToList();

        var minDuration = durations.Min();
        var maxDuration = durations.Max();
        var avgDuration = durations.Average();

        
        var expectedMinDuration = 2;
        var expectedMaxDuration = 15;
        var expectedAvgDuration = 7;

        Assert.Equal(expectedMinDuration, minDuration);
        Assert.Equal(expectedMaxDuration, maxDuration);
        Assert.Equal(expectedAvgDuration, avgDuration);
    }

    /// <summary>
    /// Test for total rental time by bike type
    /// </summary>
    [Theory]
    [InlineData(BikeType.Sport, 49)]
    [InlineData(BikeType.Mountain, 16)]
    [InlineData(BikeType.Road, 36)]
    [InlineData(BikeType.Hybrid, 13)]
    [InlineData(BikeType.Electric, 26)]
    public void TotalRentalTimeByBikeType(BikeType bikeType, int expectedTotalTime)
    {
        var actualTotalTime = fixture.Rents
            .Where(r => r.Bike.Model.Type == bikeType)
            .Sum(r => r.DurationHours);

        Assert.Equal(expectedTotalTime, actualTotalTime);
    }

    /// <summary>
    /// Test for top renters by rental count
    /// </summary>
    [Fact]
    public void TopRentersByRentalCount()
    {
        var topRenters = fixture.Rents
            .GroupBy(r => r.Renter)
            .Select(g => new
            {
                Renter = g.Key,
                RentalCount = g.Count()
            })
            .OrderByDescending(x => x.RentalCount)
            .Take(5)
            .ToList();

        var expectedCount = 5;
        var expectedTopRenterName = "Ivanov Ivan";
        var expectedMaxRentalCount = 2;

        Assert.NotNull(topRenters);
        Assert.True(topRenters.Count <= expectedCount);
        Assert.Equal(expectedCount, topRenters.Count);
        Assert.Equal(expectedTopRenterName, topRenters[0].Renter.FullName);
        Assert.Equal(expectedMaxRentalCount, topRenters[0].RentalCount);

        if (topRenters.Count > 0)
        {
            var maxRentalCount = topRenters.Max(x => x.RentalCount);
            Assert.True(topRenters[0].RentalCount == maxRentalCount);
        }
    }
}