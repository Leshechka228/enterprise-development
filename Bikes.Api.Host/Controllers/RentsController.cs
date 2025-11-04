using Bikes.Application.Contracts.Rents;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for rent management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentsController(IRentService rentService) : CrudControllerBase<RentDto, RentCreateUpdateDto>
{
    /// <summary>
    /// Get all rents
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<RentDto>>> GetAll()
    {
        try
        {
            var rents = rentService.GetAll();
            return Task.FromResult<ActionResult<List<RentDto>>>(Ok(rents));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<List<RentDto>>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Get rent by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<RentDto>> GetById(int id)
    {
        try
        {
            var rent = rentService.GetById(id);
            return Task.FromResult<ActionResult<RentDto>>(rent == null ? NotFound() : Ok(rent));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<RentDto>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    protected override Task<ActionResult<RentDto>> CreateInternal(RentCreateUpdateDto request)
    {
        var rent = rentService.Create(request);
        return Task.FromResult<ActionResult<RentDto>>(CreatedAtAction(nameof(GetById), new { id = rent.Id }, rent));
    }

    protected override Task<ActionResult<RentDto>> UpdateInternal(int id, RentCreateUpdateDto request)
    {
        var rent = rentService.Update(id, request);
        return Task.FromResult<ActionResult<RentDto>>(rent == null ? NotFound() : Ok(rent));
    }

    protected override Task<ActionResult> DeleteInternal(int id)
    {
        var result = rentService.Delete(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}