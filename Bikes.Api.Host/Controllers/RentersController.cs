using Bikes.Application.Contracts.Renters;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for renter management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentersController(IRenterService renterService) : CrudControllerBase<RenterDto, RenterCreateUpdateDto>
{
    /// <summary>
    /// Get all renters
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<RenterDto>>> GetAll()
    {
        try
        {
            var renters = renterService.GetAll();
            return Task.FromResult<ActionResult<List<RenterDto>>>(Ok(renters));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<List<RenterDto>>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Get renter by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<RenterDto>> GetById(int id)
    {
        try
        {
            var renter = renterService.GetById(id);
            return Task.FromResult<ActionResult<RenterDto>>(renter == null ? NotFound() : Ok(renter));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<RenterDto>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    protected override Task<ActionResult<RenterDto>> CreateInternal(RenterCreateUpdateDto request)
    {
        var renter = renterService.Create(request);
        return Task.FromResult<ActionResult<RenterDto>>(CreatedAtAction(nameof(GetById), new { id = renter.Id }, renter));
    }

    protected override Task<ActionResult<RenterDto>> UpdateInternal(int id, RenterCreateUpdateDto request)
    {
        var renter = renterService.Update(id, request);
        return Task.FromResult<ActionResult<RenterDto>>(renter == null ? NotFound() : Ok(renter));
    }

    protected override Task<ActionResult> DeleteInternal(int id)
    {
        var result = renterService.Delete(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}