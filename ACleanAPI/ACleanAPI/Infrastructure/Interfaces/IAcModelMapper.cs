using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Interfaces;

public interface IAcModelMapper<TModel, TEntity>
    where TModel : IAcModel
    where TEntity : IAcEntity
{
    TEntity MapToEntity(TModel model);

    TModel MapToModel(TEntity entity);
}
