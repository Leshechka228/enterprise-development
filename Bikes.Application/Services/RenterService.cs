using Bikes.Application.Contracts.Renters;
using Bikes.Domain.Models;
using Bikes.Domain.Repositories;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of renter service
/// </summary>
public class RenterService(IBikeRepository repository) : IRenterService
{
    /// <summary>
    /// Get all renters
    /// </summary>
    public List<RenterDto> GetAll()
    {
        return [.. repository.GetAllRenters().Select(r => new RenterDto
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
        var renter = repository.GetRenterById(id);
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

        var renters = repository.GetAllRenters();
        var newRenter = new Renter
        {
            Id = renters.Count > 0 ? renters.Max(r => r.Id) + 1 : 1,
            FullName = request.FullName,
            Phone = request.Phone
        };

        repository.AddRenter(newRenter);

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

        var renter = repository.GetRenterById(id);
        if (renter == null) return null;

        renter.FullName = request.FullName;
        renter.Phone = request.Phone;

        repository.UpdateRenter(renter);

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
        var renter = repository.GetRenterById(id);
        if (renter == null) return false;

        repository.DeleteRenter(id);
        return true;
    }
}