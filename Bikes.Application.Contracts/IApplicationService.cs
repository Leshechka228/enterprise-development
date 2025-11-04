namespace Bikes.Application.Contracts;

/// <summary>
/// Base interface for all application services with CRUD operations
/// </summary>
/// <typeparam name="TDto">DTO type for reading</typeparam>
/// <typeparam name="TCreateUpdateDto">DTO type for creating and updating</typeparam>
public interface IApplicationService<TDto, TCreateUpdateDto>
    where TDto : class
    where TCreateUpdateDto : class
{
    /// <summary>
    /// Get all entities
    /// </summary>
    public List<TDto> GetAll();

    /// <summary>
    /// Get entity by identifier
    /// </summary>
    public TDto? GetById(int id);

    /// <summary>
    /// Create new entity
    /// </summary>
    public TDto Create(TCreateUpdateDto request);

    /// <summary>
    /// Update entity
    /// </summary>
    public TDto? Update(int id, TCreateUpdateDto request);

    /// <summary>
    /// Delete entity
    /// </summary>
    public bool Delete(int id);
}