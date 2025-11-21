using ACleanAPI.Domain;

namespace ACleanAPI.Infrastructure;

public interface IAcGetEntityByIdRepository<TEntity>
    where TEntity : IAcEntity
{
    Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken);
}