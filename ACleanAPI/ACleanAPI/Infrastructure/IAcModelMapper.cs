using ACleanAPI.Domain;

namespace ACleanAPI.Infrastructure;

public interface IAcModelMapper<TModel, TEntity>
    where TModel : IAcModel
    where TEntity : IAcEntity
{
    TEntity MapToEntity(TModel model);
}
