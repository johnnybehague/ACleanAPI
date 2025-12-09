using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Interfaces;

public interface IAcGetEntitiesRepository<TEntity>
    where TEntity : IAcEntity
{
    Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);
}
