using Bikes.Application.Contracts.Renters;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of renter service
/// </summary>
public class RenterService(IRenterRepository renterRepository) : IRenterService
{
    /// <summary>
    /// Get all renters
    /// </summary>
    public List<RenterDto> GetAll()
    {
        return [.. renterRepository.GetAllRenters().Select(r => new RenterDto
        {
            Id = r.Id,
            FullName = r.FullName,
            Phone = r.Phone
        })];
    }

    /// <summary>
    /// Get renter by identifier
    /// </summary>
    public RenterDto? GetById(int id)
    {
        var renter = renterRepository.GetRenterById(id);
        return renter == null ? null : new RenterDto
        {
            Id = renter.Id,
            FullName = renter.FullName,
            Phone = renter.Phone
        };
    }

    /// <summary>
    /// Create new renter
    /// </summary>
    public RenterDto Create(RenterCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var renters = renterRepository.GetAllRenters();
        var maxId = renters.Count == 0 ? 0 : renters.Max(r => r.Id);

        var newRenter = new Renter
        {
            Id = maxId + 1,
            FullName = request.FullName,
            Phone = request.Phone
        };

        renterRepository.AddRenter(newRenter);

        return new RenterDto
        {
            Id = newRenter.Id,
            FullName = newRenter.FullName,
            Phone = newRenter.Phone
        };
    }

    /// <summary>
    /// Update renter
    /// </summary>
    public RenterDto? Update(int id, RenterCreateUpdateDto request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var renter = renterRepository.GetRenterById(id);
        if (renter == null) return null;

        renter.FullName = request.FullName;
        renter.Phone = request.Phone;

        renterRepository.UpdateRenter(renter);

        return new RenterDto
        {
            Id = renter.Id,
            FullName = renter.FullName,
            Phone = renter.Phone
        };
    }

    /// <summary>
    /// Delete renter
    /// </summary>
    public bool Delete(int id)
    {
        var renter = renterRepository.GetRenterById(id);
        if (renter == null) return false;

        renterRepository.DeleteRenter(id);
        return true;
    }
}