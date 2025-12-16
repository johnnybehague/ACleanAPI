using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Interfaces;

public interface IAcModelMapper<in TModel, out TEntity>
    where TModel : IAcModel
    where TEntity : IAcEntity
{
    TEntity MapToEntity(TModel model);
}
