using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Application.Interfaces;

public interface IAcEntityMapper<in TEntity, out TDto>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    TDto MapToDto(TEntity entity);
}
