using Bikes.Application.Contracts.Renters;
using Bikes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for renter management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(IRenterService renterService) : CrudControllerBase<RenterDto, RenterCreateUpdateDto>
{
    private readonly IRenterService _renterService = renterService;

    /// <summary>
    /// Get all renters
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<RenterDto>>> GetAll()
    {
        var renters = _renterService.GetAllRenters();
        return Task.FromResult<ActionResult<List<RenterDto>>>(Ok(renters));
    }

    /// <summary>
    /// Get renter by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<RenterDto>> GetById(int id)
    {
        var renter = _renterService.GetRenterById(id);
        return Task.FromResult<ActionResult<RenterDto>>(renter == null ? NotFound() : Ok(renter));
    }

    /// <summary>
    /// Create new renter
    /// </summary>
    [HttpPost]
    public override Task<ActionResult<RenterDto>> Create([FromBody] RenterCreateUpdateDto request)
    {
        var renter = _renterService.CreateRenter(request);
        return Task.FromResult<ActionResult<RenterDto>>(CreatedAtAction(nameof(GetById), new { id = renter.Id }, renter));
    }

    /// <summary>
    /// Update renter
    /// </summary>
    [HttpPut("{id}")]
    public override Task<ActionResult<RenterDto>> Update(int id, [FromBody] RenterCreateUpdateDto request)
    {
        var renter = _renterService.UpdateRenter(id, request);
        return Task.FromResult<ActionResult<RenterDto>>(renter == null ? NotFound() : Ok(renter));
    }

    /// <summary>
    /// Delete renter
    /// </summary>
    [HttpDelete("{id}")]
    public override Task<ActionResult> Delete(int id)
    {
        var result = _renterService.DeleteRenter(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}