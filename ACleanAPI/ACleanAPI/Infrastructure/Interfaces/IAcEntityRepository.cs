using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Interfaces;

public interface IAcEntityRepository<TEntity>
    where TEntity : IAcEntity
{
    Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);

    Task<TEntity?> GetEntityByIdAsync(int id, CancellationToken cancellationToken);

    Task CreateEntityAsync(TEntity entity, CancellationToken cancellationToken);

    Task UpdateEntityAsync(int entityId, TEntity entity, CancellationToken cancellationToken);

    Task DeleteEntityAsync(int id, CancellationToken cancellationToken);
}
