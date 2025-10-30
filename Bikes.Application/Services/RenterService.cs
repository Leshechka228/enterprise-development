using Bikes.Application.Contracts.Renters;
using Bikes.Domain.Models;

namespace Bikes.Application.Services;

/// <summary>
/// Implementation of renter service
/// </summary>
public class RenterService(IBikeRepository repository) : IRenterService
{
    private readonly IBikeRepository _repository = repository;

    /// <summary>
    /// Get all renters
    /// </summary>
    public List<RenterDto> GetAllRenters()
    {
        return _repository.GetAllRenters().Select(r => new RenterDto
        {
            Id = r.Id,
            FullName = r.FullName,
            Phone = r.Phone
        }).ToList();
    }

    /// <summary>
    /// Get renter by identifier
    /// </summary>
    public RenterDto? GetRenterById(int id)
    {
        var renter = _repository.GetRenterById(id);
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
    public RenterDto CreateRenter(RenterCreateUpdateDto request)
    {
        var renters = _repository.GetAllRenters();
        var newRenter = new Renter
        {
            Id = renters.Count > 0 ? renters.Max(r => r.Id) + 1 : 1,
            FullName = request.FullName,
            Phone = request.Phone
        };

        _repository.AddRenter(newRenter);

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
    public RenterDto? UpdateRenter(int id, RenterCreateUpdateDto request)
    {
        var renter = _repository.GetRenterById(id);
        if (renter == null) return null;

        renter.FullName = request.FullName;
        renter.Phone = request.Phone;

        _repository.UpdateRenter(renter);

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
    public bool DeleteRenter(int id)
    {
        var renter = _repository.GetRenterById(id);
        if (renter == null) return false;

        _repository.DeleteRenter(id);
        return true;
    }
}