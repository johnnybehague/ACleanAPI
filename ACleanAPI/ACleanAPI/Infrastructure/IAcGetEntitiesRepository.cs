using ACleanAPI.Domain;

namespace ACleanAPI.Infrastructure;

public interface IAcGetEntitiesRepository<TEntity>
    where TEntity : IAcEntity
{
    Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);
}
