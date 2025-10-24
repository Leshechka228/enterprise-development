using Bikes.Domain.Models;

namespace Bikes.Tests
{
    public class BikesTests : IClassFixture<BikesFixture>
    {
        private readonly BikesFixture _fixture;

        public BikesTests(BikesFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Test for retrieving all sport bikes information
        /// </summary>
        [Fact]
        public void GetAllSportBikes()
        {
            var sportBikes = _fixture.Bikes
                .Where(b => b.Model.Type == BikeType.Sport)
                .ToList();

            Assert.NotNull(sportBikes);
            Assert.All(sportBikes, bike => Assert.Equal(BikeType.Sport, bike.Model.Type));
        }

        /// <summary>
        /// Test for retrieving top 5 bike models by rental profit
        /// </summary>
        [Fact]
        public void Top5ModelsByProfit()
        {
            var topModelsByProfit = _fixture.Rents
                .GroupBy(r => r.Bike.Model)
                .Select(g => new
                {
                    Model = g.Key,
                    TotalProfit = g.Sum(r => r.TotalCost)
                })
                .OrderByDescending(x => x.TotalProfit)
                .Take(5)
                .ToList();

            Assert.NotNull(topModelsByProfit);
            Assert.True(topModelsByProfit.Count <= 5);
        }

        /// <summary>
        /// Test for retrieving top 5 bike models by rental duration
        /// </summary>
        [Fact]
        public void Top5ModelsByRentalDuration()
        {
            var topModelsByDuration = _fixture.Rents
                .GroupBy(r => r.Bike.Model)
                .Select(g => new
                {
                    Model = g.Key,
                    TotalDuration = g.Sum(r => r.DurationHours)
                })
                .OrderByDescending(x => x.TotalDuration)
                .Take(5)
                .ToList();

            Assert.NotNull(topModelsByDuration);
            Assert.True(topModelsByDuration.Count <= 5);
        }

        /// <summary>
        /// Test for rental statistics - min, max and average rental duration
        /// </summary>
        [Fact]
        public void RentalStatistics()
        {
            var durations = _fixture.Rents.Select(r => r.DurationHours).ToList();

            var minDuration = durations.Min();
            var maxDuration = durations.Max();
            var avgDuration = durations.Average();

            Assert.Equal(2, minDuration);
            Assert.Equal(15, maxDuration);
            Assert.Equal(7, avgDuration);
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
            var actualTotalTime = _fixture.Rents
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
            var topRenters = _fixture.Rents
                .GroupBy(r => r.Renter)
                .Select(g => new
                {
                    Renter = g.Key,
                    RentalCount = g.Count()
                })
                .OrderByDescending(x => x.RentalCount)
                .Take(5)
                .ToList();

            Assert.NotNull(topRenters);
            Assert.True(topRenters.Count <= 5);

            if (topRenters.Count > 0)
            {
                var maxRentalCount = topRenters.Max(x => x.RentalCount);
                Assert.True(topRenters[0].RentalCount == maxRentalCount);
            }
        }
    }
}