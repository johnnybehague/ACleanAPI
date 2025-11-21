using ACleanAPI.Domain;

namespace ACleanAPI.Application;

public interface IAcEntityMapper<TEntity, TDto>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    TDto MapToDto(TEntity entity);
}
