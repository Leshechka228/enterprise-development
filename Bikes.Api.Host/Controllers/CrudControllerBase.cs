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
    /// <summary>
    /// Get all entities
    /// </summary>
    [HttpGet]
    public abstract Task<ActionResult<List<TDto>>> GetAll();

    /// <summary>
    /// Get entity by id
    /// </summary>
    [HttpGet("{id}")]
    public abstract Task<ActionResult<TDto>> GetById(int id);

    /// <summary>
    /// Create new entity
    /// </summary>
    [HttpPost]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await CreateInternal(request);
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
    public virtual async Task<ActionResult<TDto>> Update(int id, [FromBody] TCreateUpdateDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await UpdateInternal(id, request);
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
    public virtual async Task<ActionResult> Delete(int id)
    {
        try
        {
            return await DeleteInternal(id);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    protected abstract Task<ActionResult<TDto>> CreateInternal(TCreateUpdateDto request);
    protected abstract Task<ActionResult<TDto>> UpdateInternal(int id, TCreateUpdateDto request);
    protected abstract Task<ActionResult> DeleteInternal(int id);
}