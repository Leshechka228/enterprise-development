using Bikes.Application.Contracts.Rents;
using Bikes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for rent management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RentsController(IRentService rentService) : CrudControllerBase<RentDto, RentCreateUpdateDto>
{
    private readonly IRentService _rentService = rentService;

    /// <summary>
    /// Get all rents
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<RentDto>>> GetAll()
    {
        var rents = _rentService.GetAllRents();
        return Task.FromResult<ActionResult<List<RentDto>>>(Ok(rents));
    }

    /// <summary>
    /// Get rent by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<RentDto>> GetById(int id)
    {
        var rent = _rentService.GetRentById(id);
        return Task.FromResult<ActionResult<RentDto>>(rent == null ? NotFound() : Ok(rent));
    }

    /// <summary>
    /// Create new rent
    /// </summary>
    [HttpPost]
    public override Task<ActionResult<RentDto>> Create([FromBody] RentCreateUpdateDto request)
    {
        var rent = _rentService.CreateRent(request);
        return Task.FromResult<ActionResult<RentDto>>(CreatedAtAction(nameof(GetById), new { id = rent.Id }, rent));
    }

    /// <summary>
    /// Update rent
    /// </summary>
    [HttpPut("{id}")]
    public override Task<ActionResult<RentDto>> Update(int id, [FromBody] RentCreateUpdateDto request)
    {
        var rent = _rentService.UpdateRent(id, request);
        return Task.FromResult<ActionResult<RentDto>>(rent == null ? NotFound() : Ok(rent));
    }

    /// <summary>
    /// Delete rent
    /// </summary>
    [HttpDelete("{id}")]
    public override Task<ActionResult> Delete(int id)
    {
        var result = _rentService.DeleteRent(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}