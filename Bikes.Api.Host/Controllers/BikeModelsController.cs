using Bikes.Application.Contracts.Models;
using Bikes.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for bike model management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeModelsController(IBikeModelService bikeModelService) : CrudControllerBase<BikeModelDto, BikeModelCreateUpdateDto>
{
    private readonly IBikeModelService _bikeModelService = bikeModelService;

    /// <summary>
    /// Get all bike models
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<BikeModelDto>>> GetAll()
    {
        var models = _bikeModelService.GetAllModels();
        return Task.FromResult<ActionResult<List<BikeModelDto>>>(Ok(models));
    }

    /// <summary>
    /// Get bike model by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<BikeModelDto>> GetById(int id)
    {
        var model = _bikeModelService.GetModelById(id);
        return Task.FromResult<ActionResult<BikeModelDto>>(model == null ? NotFound() : Ok(model));
    }

    /// <summary>
    /// Create new bike model
    /// </summary>
    [HttpPost]
    public override Task<ActionResult<BikeModelDto>> Create([FromBody] BikeModelCreateUpdateDto request)
    {
        var model = _bikeModelService.CreateModel(request);
        return Task.FromResult<ActionResult<BikeModelDto>>(CreatedAtAction(nameof(GetById), new { id = model.Id }, model));
    }

    /// <summary>
    /// Update bike model
    /// </summary>
    [HttpPut("{id}")]
    public override Task<ActionResult<BikeModelDto>> Update(int id, [FromBody] BikeModelCreateUpdateDto request)
    {
        var model = _bikeModelService.UpdateModel(id, request);
        return Task.FromResult<ActionResult<BikeModelDto>>(model == null ? NotFound() : Ok(model));
    }

    /// <summary>
    /// Delete bike model
    /// </summary>
    [HttpDelete("{id}")]
    public override Task<ActionResult> Delete(int id)
    {
        var result = _bikeModelService.DeleteModel(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}