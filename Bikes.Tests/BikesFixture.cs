using Bikes.Domain.Models;

namespace Bikes.Tests;

/// <summary>
/// Fixture for bike rental tests providing test data
/// </summary>
public class BikesFixture
{
    /// <summary>
    /// Gets the list of bike models
    /// </summary>
    public List<BikeModel> Models { get; }

    /// <summary>
    /// Gets the list of bikes
    /// </summary>
    public List<Bike> Bikes { get; }

    /// <summary>
    /// Gets the list of renters
    /// </summary>
    public List<Renter> Renters { get; }

    /// <summary>
    /// Gets the list of rental records
    /// </summary>
    public List<Rent> Rents { get; }

    /// <summary>
    /// Initializes a new instance of the BikesFixture class
    /// </summary>
    public BikesFixture()
    {
        Models = InitializeModels();
        Bikes = InitializeBikes(Models);
        Renters = InitializeRenters();
        Rents = InitializeRents(Bikes, Renters);
    }

    /// <summary>
    /// Initializes the list of bike models
    /// </summary>
    /// <returns>List of bike models</returns>
    private static List<BikeModel> InitializeModels()
    {
        return
        [
            new() { Id = 1, Name = "Sport Pro 1000", Type = BikeType.Sport, WheelSize = 28, MaxWeight = 120, Weight = 10, BrakeType = "Disc", ModelYear = 2023, PricePerHour = 15 },
            new() { Id = 2, Name = "Mountain Extreme", Type = BikeType.Mountain, WheelSize = 29, MaxWeight = 130, Weight = 12, BrakeType = "Hydraulic", ModelYear = 2023, PricePerHour = 12 },
            new() { Id = 3, Name = "Road Racer", Type = BikeType.Road, WheelSize = 26, MaxWeight = 110, Weight = 8, BrakeType = "Rim", ModelYear = 2023, PricePerHour = 10 },
            new() { Id = 4, Name = "Sport Elite", Type = BikeType.Sport, WheelSize = 27.5m, MaxWeight = 125, Weight = 11, BrakeType = "Disc", ModelYear = 2023, PricePerHour = 14 },
            new() { Id = 5, Name = "Hybrid Comfort", Type = BikeType.Hybrid, WheelSize = 28, MaxWeight = 135, Weight = 13, BrakeType = "Rim", ModelYear = 2023, PricePerHour = 8 },
            new() { Id = 6, Name = "Electric City", Type = BikeType.Electric, WheelSize = 26, MaxWeight = 140, Weight = 20, BrakeType = "Disc", ModelYear = 2023, PricePerHour = 20 },
            new() { Id = 7, Name = "Sport Lightning", Type = BikeType.Sport, WheelSize = 29, MaxWeight = 115, Weight = 9.5m, BrakeType = "Hydraulic", ModelYear = 2023, PricePerHour = 16 },
            new() { Id = 8, Name = "Mountain King", Type = BikeType.Mountain, WheelSize = 27.5m, MaxWeight = 128, Weight = 11.5m, BrakeType = "Disc", ModelYear = 2023, PricePerHour = 13 },
            new() { Id = 9, Name = "Road Speed", Type = BikeType.Road, WheelSize = 28, MaxWeight = 105, Weight = 7.5m, BrakeType = "Rim", ModelYear = 2023, PricePerHour = 11 },
            new() { Id = 10, Name = "Sport Thunder", Type = BikeType.Sport, WheelSize = 26, MaxWeight = 122, Weight = 10.5m, BrakeType = "Disc", ModelYear = 2023, PricePerHour = 15.5m },
            new() { Id = 11, Name = "Electric Mountain", Type = BikeType.Electric, WheelSize = 29, MaxWeight = 145, Weight = 22, BrakeType = "Hydraulic", ModelYear = 2023, PricePerHour = 25 }
        ];
    }

    /// <summary>
    /// Initializes the list of bikes
    /// </summary>
    /// <param name="models">List of bike models</param>
    /// <returns>List of bikes</returns>
    private static List<Bike> InitializeBikes(List<BikeModel> models)
    {
        var bikes = new List<Bike>();
        var colors = new[] { "Red", "Blue", "Green", "Black", "White", "Yellow" };

        // ‘иксированное распределение моделей дл€ предсказуемости
        var bikeConfigurations = new[]
        {
            new { ModelIndex = 0, ColorIndex = 0 }, // Sport Pro 1000 - Red
            new { ModelIndex = 1, ColorIndex = 1 }, // Mountain Extreme - Blue
            new { ModelIndex = 2, ColorIndex = 2 }, // Road Racer - Green
            new { ModelIndex = 3, ColorIndex = 3 }, // Sport Elite - Black
            new { ModelIndex = 4, ColorIndex = 4 }, // Hybrid Comfort - White
            new { ModelIndex = 5, ColorIndex = 5 }, // Electric City - Yellow
            new { ModelIndex = 6, ColorIndex = 0 }, // Sport Lightning - Red
            new { ModelIndex = 7, ColorIndex = 1 }, // Mountain King - Blue
            new { ModelIndex = 8, ColorIndex = 2 }, // Road Speed - Green
            new { ModelIndex = 9, ColorIndex = 3 }, // Sport Thunder - Black
            new { ModelIndex = 10, ColorIndex = 4 }, // Electric Mountain - White
            new { ModelIndex = 0, ColorIndex = 5 }, // Sport Pro 1000 - Yellow
            new { ModelIndex = 1, ColorIndex = 0 }, // Mountain Extreme - Red
            new { ModelIndex = 2, ColorIndex = 1 }, // Road Racer - Blue
            new { ModelIndex = 3, ColorIndex = 2 }  // Sport Elite - Green
        };

        for (int i = 0; i < bikeConfigurations.Length; i++)
        {
            var config = bikeConfigurations[i];
            var model = models[config.ModelIndex];
            var color = colors[config.ColorIndex];

            bikes.Add(new Bike
            {
                Id = i + 1,
                SerialNumber = $"SN{(i + 1):D6}",
                Model = model,
                Color = color,
                IsAvailable = i % 2 == 0 // „етные доступны, нечетные - нет
            });
        }

        return bikes;
    }

    /// <summary>
    /// Initializes the list of renters
    /// </summary>
    /// <returns>List of renters</returns>
    private static List<Renter> InitializeRenters()
    {
        return
        [
            new() { Id = 1, FullName = "Ivanov Ivan", Phone = "+79111111111" },
            new() { Id = 2, FullName = "Petrov Petr", Phone = "+79112222222" },
            new() { Id = 3, FullName = "Sidorov Alexey", Phone = "+79113333333" },
            new() { Id = 4, FullName = "Kuznetsova Maria", Phone = "+79114444444" },
            new() { Id = 5, FullName = "Smirnov Dmitry", Phone = "+79115555555" },
            new() { Id = 6, FullName = "Vasilyeva Ekaterina", Phone = "+79116666666" },
            new() { Id = 7, FullName = "Popov Artem", Phone = "+79117777777" },
            new() { Id = 8, FullName = "Lebedeva Olga", Phone = "+79118888888" },
            new() { Id = 9, FullName = "Novikov Sergey", Phone = "+79119999999" },
            new() { Id = 10, FullName = "Morozova Anna", Phone = "+79110000000" },
            new() { Id = 11, FullName = "Volkov Pavel", Phone = "+79121111111" },
            new() { Id = 12, FullName = "Sokolova Irina", Phone = "+79122222222" }
        ];
    }

    /// <summary>
    /// Initializes the list of rental records
    /// </summary>
    private static List<Rent> InitializeRents(List<Bike> bikes, List<Renter> renters)
    {
        var rents = new List<Rent>();
        var rentId = 1;

        var rentalData = new[]
        {
            new { BikeIndex = 0, RenterIndex = 0, Duration = 5, DaysAgo = 2 },
            new { BikeIndex = 1, RenterIndex = 1, Duration = 3, DaysAgo = 5 },
            new { BikeIndex = 2, RenterIndex = 2, Duration = 8, DaysAgo = 1 },
            new { BikeIndex = 0, RenterIndex = 3, Duration = 2, DaysAgo = 7 },
            new { BikeIndex = 3, RenterIndex = 4, Duration = 12, DaysAgo = 3 },
            new { BikeIndex = 1, RenterIndex = 5, Duration = 6, DaysAgo = 4 },
            new { BikeIndex = 4, RenterIndex = 6, Duration = 4, DaysAgo = 6 },
            new { BikeIndex = 2, RenterIndex = 7, Duration = 10, DaysAgo = 2 },
            new { BikeIndex = 5, RenterIndex = 8, Duration = 7, DaysAgo = 1 },
            new { BikeIndex = 3, RenterIndex = 9, Duration = 3, DaysAgo = 8 },
            new { BikeIndex = 6, RenterIndex = 10, Duration = 15, DaysAgo = 2 },
            new { BikeIndex = 4, RenterIndex = 11, Duration = 9, DaysAgo = 5 },
            new { BikeIndex = 7, RenterIndex = 0, Duration = 2, DaysAgo = 3 },
            new { BikeIndex = 5, RenterIndex = 1, Duration = 6, DaysAgo = 4 },
            new { BikeIndex = 8, RenterIndex = 2, Duration = 11, DaysAgo = 1 },
            new { BikeIndex = 6, RenterIndex = 3, Duration = 4, DaysAgo = 7 },
            new { BikeIndex = 9, RenterIndex = 4, Duration = 8, DaysAgo = 2 },
            new { BikeIndex = 7, RenterIndex = 5, Duration = 5, DaysAgo = 5 },
            new { BikeIndex = 10, RenterIndex = 6, Duration = 13, DaysAgo = 3 },
            new { BikeIndex = 8, RenterIndex = 7, Duration = 7, DaysAgo = 4 }
        };

        foreach (var data in rentalData)
        {
            var bike = bikes[data.BikeIndex];
            var renter = renters[data.RenterIndex];

            rents.Add(new Rent
            {
                Id = rentId++,
                Bike = bike,
                Renter = renter,
                StartTime = DateTime.Now.AddDays(-data.DaysAgo),
                DurationHours = data.Duration
            });
        }

        return rents;
    }
}