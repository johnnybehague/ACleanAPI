using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Infrastructure.Interfaces;

/// <summary>
/// Entity / Model mapper interface. This is used to map between entities and models.
/// </summary>
/// <typeparam name="TModel">Model</typeparam>
/// <typeparam name="TEntity">Entity</typeparam>
public interface IAcModelMapper<TModel, TEntity>
    where TModel : IAcModel
    where TEntity : IAcEntity
{
    /// <summary>
    /// Maps the specified model to its entity.
    /// </summary>
    /// <param name="entity">The model instance to map to the specified entity. Cannot be <c>null</c>.</param>
    /// <returns>The specified entity of the model.</returns>
    TEntity MapToEntity(TModel model);

    /// <summary>
    /// Maps the specified entity to its model.
    /// </summary>
    /// <param name="entity">The entity instance to map to the specified model. Cannot be <c>null</c>.</param>
    /// <returns>The specified model of the entity.</returns>
    TModel MapToModel(TEntity entity);
}
