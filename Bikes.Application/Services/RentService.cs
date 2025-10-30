using Bikes.Application.Contracts.Rents;
using Bikes.Domain.Models;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of rent service
/// </summary>
public class RentService(IBikeRepository repository) : IRentService
{
    private readonly IBikeRepository _repository = repository;

    /// <summary>
    /// Get all rents
    /// </summary>
    public List<RentDto> GetAllRents()
    {
        return _repository.GetAllRents().Select(r => new RentDto
        {
            Id = r.Id,
            BikeId = r.Bike.Id,
            RenterId = r.Renter.Id,
            StartTime = r.StartTime,
            DurationHours = r.DurationHours,
            TotalCost = r.TotalCost
        }).ToList();
    }

    /// <summary>
    /// Get rent by identifier
    /// </summary>
    public RentDto? GetRentById(int id)
    {
        var rent = _repository.GetAllRents().FirstOrDefault(r => r.Id == id);
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
    public RentDto CreateRent(RentCreateUpdateDto request)
    {
        var bikes = _repository.GetAllBikes();
        var renters = _repository.GetAllRenters();
        var rents = _repository.GetAllRents();

        var bike = bikes.FirstOrDefault(b => b.Id == request.BikeId);
        var renter = renters.FirstOrDefault(r => r.Id == request.RenterId);

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
    public RentDto? UpdateRent(int id, RentCreateUpdateDto request)
    {
        var rents = _repository.GetAllRents();
        var rent = rents.FirstOrDefault(r => r.Id == id);
        if (rent == null) return null;

        var bikes = _repository.GetAllBikes();
        var renters = _repository.GetAllRenters();

        var bike = bikes.FirstOrDefault(b => b.Id == request.BikeId);
        var renter = renters.FirstOrDefault(r => r.Id == request.RenterId);

        if (bike == null)
            throw new InvalidOperationException("Bike not found");
        if (renter == null)
            throw new InvalidOperationException("Renter not found");

        rent.Bike = bike;
        rent.Renter = renter;
        rent.StartTime = request.StartTime;
        rent.DurationHours = request.DurationHours;

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
    public bool DeleteRent(int id)
    {
        var rents = _repository.GetAllRents();
        var rent = rents.FirstOrDefault(r => r.Id == id);
        return rent != null;
    }
}