using ACleanAPI.Domain.Core;
using ACleanAPI.Infrastructure.Core;
using ACleanAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ACleanAPI.Infrastructure.Repositories;

/// <summary>
/// Base repository class for managing entities of a specific type. 
/// </summary>
/// <remarks>
/// This class provides common logic for handling CRUD operations for entities.
/// </remarks>
/// <typeparam name="TModel">Model</typeparam>
/// <typeparam name="TEntity">Entity</typeparam>
public abstract class AcEntityRepositoryBase<TModel, TEntity> : IAcEntityRepository<TEntity>
    where TModel : AcModelBase
    where TEntity : AcEntityBase
{
    /// <summary>
    /// A reference to the database context used for data access operations.
    /// </summary>
    private readonly DbContext _context;

    /// <summary>
    /// The mapper used to convert between <typeparamref name="TEntity"/> and <typeparamref name="TModel"/> objects.
    /// </summary>
    private IAcModelMapper<TModel, TEntity> _mapper { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="AcEntityRepositoryBase{TModel, TEntity}"/> class with the specified database context and model mapper.
    /// </summary>
    /// <remarks>This constructor is intended for use by derived repository classes to provide data access and
    /// mapping functionality.</remarks>
    /// <param name="context">The <see cref="DbContext"/> used to access the underlying database. Cannot be <see langword="null"/>.</param>
    /// <param name="mapper">The model mapper that converts between <typeparamref name="TModel"/> and <typeparamref name="TEntity"/>
    /// instances. Cannot be <see langword="null"/>.</param>
    protected AcEntityRepositoryBase(DbContext context, IAcModelMapper<TModel, TEntity> mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region Entities

    /// <summary>
    /// Asynchronously get all entities of a specific type.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An IEnumerable of <typeparamref name="TEntity"/>.</returns>
    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        var data = await GetModelsAsync(cancellationToken);
        return data.Select(_mapper.MapToEntity).ToList();
    }

    /// <summary>
    /// Asynchronously get a single entity by its unique id.
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A <typeparamref name="TEntity"/>.</returns>
    public async Task<TEntity?> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await GetModelByIdAsync(id, cancellationToken);
        return  data != null ? _mapper.MapToEntity(data) : null;
    }

    /// <summary>
    /// Asynchronously create a new entity of a specific type.
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Created <typeparamref name="TEntity"/> value.</returns>
    public async Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.MapToModel(entity);
        var createdModel = await CreateModelAsync(model, cancellationToken);
        return _mapper.MapToEntity(createdModel ?? model);
    }

    /// <summary>
    /// Asynchronously update an existing entity of a specific type.
    /// </summary>
    /// <param name="entityId">Entity Id</param>
    /// <param name="entity">Entity</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Updated <typeparamref name="TEntity"/> value.</returns>
    public async Task<TEntity> UpdateEntityAsync(int entityId, TEntity entity,  CancellationToken cancellationToken)
    {
        var model = _mapper.MapToModel(entity);
        var updatedModel = await UpdateModelAsync(entityId, model, cancellationToken);
        return _mapper.MapToEntity(updatedModel);
    }

    /// <summary>
    /// Asynchronously delete an existing entity of a specific type by its unique id.
    /// </summary>
    /// <param name="id">Entity Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    [ExcludeFromCodeCoverage]
    public async Task DeleteEntityAsync(int id, CancellationToken cancellationToken)
    {
        await DeleteModelAsync(id, cancellationToken);
    }

    #endregion

    #region Models

    /// <summary>
    /// Asynchronously get all models of a specific type.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>An IEnumerable of <typeparamref name="TModel"/>.</returns>
    [ExcludeFromCodeCoverage]
    private async Task<IEnumerable<TModel>> GetModelsAsync(CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        return await dbSet.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Asynchronously get a single model by its unique id.
    /// </summary>
    /// <param name="id">Model Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>A <typeparamref name="TModel"/>.</returns>
    [ExcludeFromCodeCoverage]
    private async Task<TModel?> GetModelByIdAsync(int id, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        return await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>
    /// Asynchronously create a new model of a specific type.
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Created <typeparamref name="TModel"/> value.</returns>
    [ExcludeFromCodeCoverage]
    private async Task<TModel> CreateModelAsync(TModel model, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        var result = await dbSet.AddAsync(model, cancellationToken);
        return result.Entity;
    }

    /// <summary>
    /// Asynchronously update an existing model of a specific type.
    /// </summary>
    /// <param name="id">Model Id</param>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Updated <typeparamref name="TEntity"/> value.</returns>
    [ExcludeFromCodeCoverage]
    public async Task<TModel> UpdateModelAsync(int id, TModel model, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        var existingModel = await dbSet.FindAsync([id], cancellationToken);
        if (existingModel != null)
        {
            existingModel.UpdateFrom(model);
        }
        return existingModel ?? model;
    }

    /// <summary>
    /// Asynchronously delete an existing model of a specific type by its unique id.
    /// </summary>
    /// <param name="id">Model Id</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    [ExcludeFromCodeCoverage]
    public async Task DeleteModelAsync(int id, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (entity != null)
        {
            dbSet.Remove(entity);
        }
    }

    #endregion

    /// <summary>
    /// Gets the <see cref="DbSet{TModel}"/> from the database context using reflection.
    /// </summary>
    /// <remarks>
    /// This method assumes that there is only one property of type <see cref="DbSet{TModel}"/> in the context. If no such property is found, an exception is thrown.
    /// </remarks>
    /// <returns></returns>
    [ExcludeFromCodeCoverage]
    private DbSet<TModel> GetDbSet()
    {
        var property = _context.GetType()
            .GetProperties()
            .First(p => p.PropertyType == typeof(DbSet<TModel>));

        if (property == null)
            throw new InvalidOperationException($"No DbSet<{typeof(TModel).Name}> found in {_context.GetType().Name}");

        var propertyInfo = property.GetValue(_context);
        if(propertyInfo == null)
            throw new InvalidOperationException($"Failed to retrieve DbSet<{typeof(TModel).Name}> from {_context.GetType().Name}");

        return (DbSet<TModel>)propertyInfo;
    }
}
