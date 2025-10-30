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
        var renter = _repository.GetAllRenters().FirstOrDefault(r => r.Id == id);
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
            Id = renters.Max(r => r.Id) + 1,
            FullName = request.FullName,
            Phone = request.Phone
        };

        // Note: In real application, we would add to repository
        // _repository.AddRenter(newRenter);

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
        var renters = _repository.GetAllRenters();
        var renter = renters.FirstOrDefault(r => r.Id == id);
        if (renter == null) return null;

        renter.FullName = request.FullName;
        renter.Phone = request.Phone;

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
        var renters = _repository.GetAllRenters();
        var renter = renters.FirstOrDefault(r => r.Id == id);
        return renter != null;
        // Note: In real application, we would remove from repository
        // return _repository.DeleteRenter(id);
    }
}