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
    public abstract Task<ActionResult<TDto>> Create([FromBody] TCreateUpdateDto request);

    /// <summary>
    /// Update entity
    /// </summary>
    [HttpPut("{id}")]
    public abstract Task<ActionResult<TDto>> Update(int id, [FromBody] TCreateUpdateDto request);

    /// <summary>
    /// Delete entity
    /// </summary>
    [HttpDelete("{id}")]
    public abstract Task<ActionResult> Delete(int id);
}