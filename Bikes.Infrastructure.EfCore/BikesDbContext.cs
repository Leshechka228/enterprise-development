using Microsoft.EntityFrameworkCore;
using Bikes.Domain.Models;

namespace Bikes.Infrastructure.EfCore;

/// <summary>
/// Database context for bikes rental system
/// </summary>
public class BikesDbContext : DbContext
{
    public BikesDbContext(DbContextOptions<BikesDbContext> options) : base(options)
    {
    }

    /// <summary>
    /// Bike models table
    /// </summary>
    public DbSet<BikeModel> BikeModels { get; set; } = null!;

    /// <summary>
    /// Bikes table
    /// </summary>
    public DbSet<Bike> Bikes { get; set; } = null!;

    /// <summary>
    /// Renters table
    /// </summary>
    public DbSet<Renter> Renters { get; set; } = null!;

    /// <summary>
    /// Rents table
    /// </summary>
    public DbSet<Rent> Rents { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // BikeModel configuration
        modelBuilder.Entity<BikeModel>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).IsRequired().HasConversion<string>();
            entity.Property(e => e.WheelSize).HasPrecision(5, 2);
            entity.Property(e => e.MaxWeight).HasPrecision(7, 2);
            entity.Property(e => e.Weight).HasPrecision(7, 2);
            entity.Property(e => e.BrakeType).IsRequired().HasMaxLength(50);
            entity.Property(e => e.PricePerHour).HasPrecision(10, 2);
        });

        // Bike configuration
        modelBuilder.Entity<Bike>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.SerialNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Color).IsRequired().HasMaxLength(30);

            entity.HasOne(e => e.Model)
                  .WithMany(m => m.Bikes)
                  .HasForeignKey(e => e.ModelId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Renter configuration
        modelBuilder.Entity<Renter>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
        });

        // Rent configuration
        modelBuilder.Entity<Rent>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StartTime).IsRequired();
            entity.Property(e => e.DurationHours).IsRequired();

            entity.HasOne(e => e.Bike)
                  .WithMany(b => b.Rents)
                  .HasForeignKey(e => e.BikeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(e => e.Renter)
                  .WithMany(r => r.Rents)
                  .HasForeignKey(e => e.RenterId)
                  .OnDelete(DeleteBehavior.Restrict);
        });
    }
}