using Bikes.Application.Contracts.Rents;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of rent service
/// </summary>
public class RentService(
    IRentRepository rentRepository,
    IBikeRepository bikeRepository,
    IRenterRepository renterRepository) : IRentService
{
    /// <summary>
    /// Get all rents
    /// </summary>
    public List<RentDto> GetAll()
    {
        return [.. rentRepository.GetAllRents().Select(r => new RentDto
        {
            Id = r.Id,
            BikeId = r.Bike.Id,
            RenterId = r.Renter.Id,
            StartTime = r.StartTime,
            DurationHours = r.DurationHours,
            TotalCost = r.Bike.Model.PricePerHour * r.DurationHours
        })];
    }

    /// <summary>
    /// Get rent by identifier
    /// </summary>
    public RentDto? GetById(int id)
    {
        var rent = rentRepository.GetRentById(id);
        return rent == null ? null : new RentDto
        {
            Id = rent.Id,
            BikeId = rent.Bike.Id,
            RenterId = rent.Renter.Id,
            StartTime = rent.StartTime,
            DurationHours = rent.DurationHours,
            TotalCost = rent.Bike.Model.PricePerHour * rent.DurationHours
        };
    }

    /// <summary>
    /// Create new rent
    /// </summary>
    public RentDto Create(RentCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var bike = bikeRepository.GetBikeById(request.BikeId);
        var renter = renterRepository.GetRenterById(request.RenterId);
        var rents = rentRepository.GetAllRents();

        if (bike == null)
            throw new InvalidOperationException("Bike not found");
        if (renter == null)
            throw new InvalidOperationException("Renter not found");

        var maxId = rents.Count == 0 ? 0 : rents.Max(r => r.Id);

        var newRent = new Rent
        {
            Id = maxId + 1,
            Bike = bike,
            Renter = renter,
            StartTime = request.StartTime,
            DurationHours = request.DurationHours
        };

        rentRepository.AddRent(newRent);

        return new RentDto
        {
            Id = newRent.Id,
            BikeId = newRent.Bike.Id,
            RenterId = newRent.Renter.Id,
            StartTime = newRent.StartTime,
            DurationHours = newRent.DurationHours,
            TotalCost = newRent.Bike.Model.PricePerHour * newRent.DurationHours
        };
    }

    /// <summary>
    /// Update rent
    /// </summary>
    public RentDto? Update(int id, RentCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var rent = rentRepository.GetRentById(id);
        if (rent == null) return null;

        var bike = bikeRepository.GetBikeById(request.BikeId)
            ?? throw new InvalidOperationException("Bike not found");
        var renter = renterRepository.GetRenterById(request.RenterId)
            ?? throw new InvalidOperationException("Renter not found");

        rent.Bike = bike;
        rent.Renter = renter;
        rent.StartTime = request.StartTime;
        rent.DurationHours = request.DurationHours;

        rentRepository.UpdateRent(rent);

        return new RentDto
        {
            Id = rent.Id,
            BikeId = rent.Bike.Id,
            RenterId = rent.Renter.Id,
            StartTime = rent.StartTime,
            DurationHours = rent.DurationHours,
            TotalCost = rent.Bike.Model.PricePerHour * rent.DurationHours
        };
    }

    /// <summary>
    /// Delete rent
    /// </summary>
    public bool Delete(int id)
    {
        var rent = rentRepository.GetRentById(id);
        if (rent == null) return false;

        rentRepository.DeleteRent(id);
        return true;
    }
}