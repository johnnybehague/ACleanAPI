using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Application.Interfaces;

public interface IAcEntityMapper<TEntity, TDto>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    TDto MapToDto(TEntity entity);

    TEntity MapToEntity(TDto dto);
}
