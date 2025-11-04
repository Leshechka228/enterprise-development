using Bikes.Application.Contracts.Rents;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of rent service
/// </summary>
public class RentService(IBikeRepository repository) : IRentService
{
    /// <summary>
    /// Get all rents
    /// </summary>
    public List<RentDto> GetAll()
    {
        return [.. repository.GetAllRents().Select(r => new RentDto
        {
            Id = r.Id,
            BikeId = r.Bike.Id,
            RenterId = r.Renter.Id,
            StartTime = r.StartTime,
            DurationHours = r.DurationHours,
            TotalCost = r.TotalCost
        })];
    }

    /// <summary>
    /// Get rent by identifier
    /// </summary>
    public RentDto? GetById(int id)
    {
        var rent = repository.GetRentById(id);
        return rent == null ? null : new RentDto
        {
            Id = rent.Id,
            BikeId = rent.Bike.Id,
            RenterId = rent.Renter.Id,
            StartTime = rent.StartTime,
            DurationHours = rent.DurationHours,
            TotalCost = rent.TotalCost
        };
    }

    /// <summary>
    /// Create new rent
    /// </summary>
    public RentDto Create(RentCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var bike = repository.GetBikeById(request.BikeId);
        var renter = repository.GetRenterById(request.RenterId);
        var rents = repository.GetAllRents();

        if (bike == null)
            throw new InvalidOperationException("Bike not found");
        if (renter == null)
            throw new InvalidOperationException("Renter not found");

        var newRent = new Rent
        {
            Id = rents.Max(r => r.Id) + 1,
            Bike = bike,
            Renter = renter,
            StartTime = request.StartTime,
            DurationHours = request.DurationHours
        };

        repository.AddRent(newRent);

        return new RentDto
        {
            Id = newRent.Id,
            BikeId = newRent.Bike.Id,
            RenterId = newRent.Renter.Id,
            StartTime = newRent.StartTime,
            DurationHours = newRent.DurationHours,
            TotalCost = newRent.TotalCost
        };
    }

    /// <summary>
    /// Update rent
    /// </summary>
    public RentDto? Update(int id, RentCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rent = repository.GetRentById(id);
        if (rent == null) return null;

        var bike = repository.GetBikeById(request.BikeId);
        var renter = repository.GetRenterById(request.RenterId);

        if (bike == null)
            throw new InvalidOperationException("Bike not found");
        if (renter == null)
            throw new InvalidOperationException("Renter not found");

        rent.Bike = bike;
        rent.Renter = renter;
        rent.StartTime = request.StartTime;
        rent.DurationHours = request.DurationHours;

        repository.UpdateRent(rent);

        return new RentDto
        {
            Id = rent.Id,
            BikeId = rent.Bike.Id,
            RenterId = rent.Renter.Id,
            StartTime = rent.StartTime,
            DurationHours = rent.DurationHours,
            TotalCost = rent.TotalCost
        };
    }

    /// <summary>
    /// Delete rent
    /// </summary>
    public bool Delete(int id)
    {
        var rent = repository.GetRentById(id);
        if (rent == null) return false;

        repository.DeleteRent(id);
        return true;
    }
}