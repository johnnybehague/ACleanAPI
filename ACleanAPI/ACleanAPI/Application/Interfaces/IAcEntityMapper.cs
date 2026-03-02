using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Application.Interfaces;

/// <summary>
/// Entity / DTO mapper interface. This is used to map between entities and DTOs.
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TDto">DTO</typeparam>
public interface IAcEntityMapper<TEntity, TDto>
    where TEntity : IAcEntity
    where TDto : IAcEntityDto
{
    /// <summary>
    /// Maps the specified entity to its corresponding data transfer object (DTO).
    /// </summary>
    /// <param name="entity">The entity instance to map to a DTO. Cannot be <c>null</c>.</param>
    /// <returns>The DTO representation of the specified entity.</returns>
    TDto MapToDto(TEntity entity);

    /// <summary>
    /// Maps the specified data transfer object (DTO) to its corresponding entity.
    /// </summary>
    /// <param name="dto">The DTO representation to map to an Entity. Cannot be <c>null</c>.</param>
    /// <returns>The specified entity of the DTO representation.</returns>
    TEntity MapToEntity(TDto dto);
}
