using Bikes.Application.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Controller for bike model management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BikeModelsController(IBikeModelService bikeModelService) : CrudControllerBase<BikeModelDto, BikeModelCreateUpdateDto>
{
    /// <summary>
    /// Get all bike models
    /// </summary>
    [HttpGet]
    public override Task<ActionResult<List<BikeModelDto>>> GetAll()
    {
        try
        {
            var models = bikeModelService.GetAll();
            return Task.FromResult<ActionResult<List<BikeModelDto>>>(Ok(models));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<List<BikeModelDto>>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    /// <summary>
    /// Get bike model by id
    /// </summary>
    [HttpGet("{id}")]
    public override Task<ActionResult<BikeModelDto>> GetById(int id)
    {
        try
        {
            var model = bikeModelService.GetById(id);
            return Task.FromResult<ActionResult<BikeModelDto>>(model == null ? NotFound() : Ok(model));
        }
        catch (Exception ex)
        {
            return Task.FromResult<ActionResult<BikeModelDto>>(StatusCode(500, $"Internal server error: {ex.Message}"));
        }
    }

    protected override Task<ActionResult<BikeModelDto>> CreateInternal(BikeModelCreateUpdateDto request)
    {
        var model = bikeModelService.Create(request);
        return Task.FromResult<ActionResult<BikeModelDto>>(CreatedAtAction(nameof(GetById), new { id = model.Id }, model));
    }

    protected override Task<ActionResult<BikeModelDto>> UpdateInternal(int id, BikeModelCreateUpdateDto request)
    {
        var model = bikeModelService.Update(id, request);
        return Task.FromResult<ActionResult<BikeModelDto>>(model == null ? NotFound() : Ok(model));
    }

    protected override Task<ActionResult> DeleteInternal(int id)
    {
        var result = bikeModelService.Delete(id);
        return Task.FromResult<ActionResult>(result ? NoContent() : NotFound());
    }
}