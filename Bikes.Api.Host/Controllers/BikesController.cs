using Bikes.Application.Contracts.Bikes;
using Bikes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for bike management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikesController(IBikeService bikeService) : CrudControllerBase<BikeDto, BikeCreateUpdateDto>
{
    private readonly IBikeService _bikeService = bikeService;

    /// <summary>
    /// Get all bikes
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<BikeDto>>> GetAll()
    {
        var bikes = _bikeService.GetAllBikes();
        return Task.FromResult<ActionResult<List<BikeDto>>>(Ok(bikes));
    }

    /// <summary>
    /// Get bike by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<BikeDto>> GetById(int id)
    {
        var bike = _bikeService.GetBikeById(id);
        return Task.FromResult<ActionResult<BikeDto>>(bike == null ? NotFound() : Ok(bike));
    }

    /// <summary>
    /// Create new bike
    /// </summary>
    [HttpPost]
    public override Task<ActionResult<BikeDto>> Create([FromBody] BikeCreateUpdateDto request)
    {
        var bike = _bikeService.CreateBike(request);
        return Task.FromResult<ActionResult<BikeDto>>(CreatedAtAction(nameof(GetById), new { id = bike.Id }, bike));
    }

    /// <summary>
    /// Update bike
    /// </summary>
    [HttpPut("{id}")]
    public override Task<ActionResult<BikeDto>> Update(int id, [FromBody] BikeCreateUpdateDto request)
    {
        var bike = _bikeService.UpdateBike(id, request);
        return Task.FromResult<ActionResult<BikeDto>>(bike == null ? NotFound() : Ok(bike));
    }

    /// <summary>
    /// Delete bike
    /// </summary>
    [HttpDelete("{id}")]
    public override Task<ActionResult> Delete(int id)
    {
        var result = _bikeService.DeleteBike(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}