using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Interfaces;

/// <summary>
/// Repository interface for managing entities of a specific type.
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
public interface IAcEntityRepository<TEntity>
    where TEntity : IAcEntity
{
    /// <summary>
    /// Asynchronously get all entities of a specific type.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An IEnumerable of <typeparamref name="TEntity"/>.</returns>
    Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously get a single entity by its unique id.
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A <typeparamref name="TEntity"/>.</returns>
    Task<TEntity?> GetEntityByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously create a new entity of a specific type.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    Task CreateEntityAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously update an existing entity of a specific type.
    /// </summary>
    /// <param name="entityId">Entity Id</param>
    /// <param name="entity">Entity</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    Task UpdateEntityAsync(int entityId, TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Asynchronously delete an existing entity of a specific type by its unique id.
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    Task DeleteEntityAsync(int id, CancellationToken cancellationToken);
}
