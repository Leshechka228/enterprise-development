using Bikes.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bikes.Api.Host.Controllers;

/// <summary>
/// Base controller for CRUD operations
/// </summary>
/// <typeparam name="TDto">DTO type</typeparam>
/// <typeparam name="TCreateUpdateDto">Create/Update DTO type</typeparam>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto> : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
{
    protected abstract IApplicationService<TDto, TCreateUpdateDto> Service { get; }

    /// <summary>
    /// Get all entities
    /// </summary>
    [HttpGet]
    public virtual ActionResult<List<TDto>> GetAll()
    {
        try
        {
            var entities = Service.GetAll();
            return Ok(entities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Get entity by id
    /// </summary>
    [HttpGet("{id}")]
    public virtual ActionResult<TDto> GetById(int id)
    {
        try
        {
            var entity = Service.GetById(id);
            return entity == null ? NotFound() : Ok(entity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Create new entity
    /// </summary>
    [HttpPost]
    public virtual ActionResult<TDto> Create([FromBody] TCreateUpdateDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Service.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = GetEntityId(entity) }, entity);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Update entity
    /// </summary>
    [HttpPut("{id}")]
    public virtual ActionResult<TDto> Update(int id, [FromBody] TCreateUpdateDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var entity = Service.Update(id, request);
            return entity == null ? NotFound() : Ok(entity);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Delete entity
    /// </summary>
    [HttpDelete("{id}")]
    public virtual ActionResult Delete(int id)
    {
        try
        {
            var result = Service.Delete(id);
            return result ? NoContent() : NotFound();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    /// <summary>
    /// Extract entity ID for CreatedAtAction
    /// </summary>
    private static int GetEntityId(TDto entity)
    {
        var idProperty = typeof(TDto).GetProperty("Id");
        return idProperty != null ? (int)idProperty.GetValue(entity)! : 0;
    }
}