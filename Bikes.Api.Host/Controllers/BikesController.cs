using Bikes.Application.Contracts.Bikes;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for bike management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikesController(IBikeService bikeService) : CrudControllerBase<BikeDto, BikeCreateUpdateDto>
{
    /// <summary>
    /// Get all bikes
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<BikeDto>>> GetAll()
    {
        try
        {
            var bikes = bikeService.GetAll();
            return Task.FromResult<ActionResult<List<BikeDto>>>(Ok(bikes));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<List<BikeDto>>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Get bike by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<BikeDto>> GetById(int id)
    {
        try
        {
            var bike = bikeService.GetById(id);
            return Task.FromResult<ActionResult<BikeDto>>(bike == null ? NotFound() : Ok(bike));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<BikeDto>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    protected override Task<ActionResult<BikeDto>> CreateInternal(BikeCreateUpdateDto request)
    {
        var bike = bikeService.Create(request);
        return Task.FromResult<ActionResult<BikeDto>>(CreatedAtAction(nameof(GetById), new { id = bike.Id }, bike));
    }

    protected override Task<ActionResult<BikeDto>> UpdateInternal(int id, BikeCreateUpdateDto request)
    {
        var bike = bikeService.Update(id, request);
        return Task.FromResult<ActionResult<BikeDto>>(bike == null ? NotFound() : Ok(bike));
    }

    protected override Task<ActionResult> DeleteInternal(int id)
    {
        var result = bikeService.Delete(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}