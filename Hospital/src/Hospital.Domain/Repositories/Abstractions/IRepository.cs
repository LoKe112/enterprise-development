namespace Hospital.Domain.Repositories.Abstractions;

/// <summary>
/// Generic repository interface for performing CRUD operations on entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the entity.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Creates a new entity in the repository.
    /// </summary>
    /// <param name="entity">The entity to create.</param>
    /// <returns>The unique identifier of the newly created entity.</returns>
    public Task<T> CreateAsync(T entity);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    /// <returns>A list of all entities of type <typeparamref name="T"/>.</returns>
    public Task<List<T>> GetAllAsync();

    /// <summary>
    /// Retrieves a single entity by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity.</param>
    /// <returns>The entity with the specified id, or <c>null</c> if not found.</returns>
    public Task<T?> GetByIdAsync(Guid id);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    /// <param name="entity">The entity data to update.</param>
    /// <returns>The updated entity, or <c>null</c> if the entity does not exist.</returns>
    public Task<T?> UpdateAsync(T entity);

    /// <summary>
    /// Deletes an entity from the repository by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the entity to delete.</param>
    /// <returns><c>true</c> if the entity was successfully deleted; otherwise, <c>false</c>.</returns>
    public Task<bool> DeleteAsync(Guid id);
}
