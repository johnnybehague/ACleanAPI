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
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    public async Task CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
    {
        var model = _mapper.MapToModel(entity);
        await CreateModelAsync(model, cancellationToken);
    }

    /// <summary>
    /// Asynchronously update an existing entity of a specific type.
    /// </summary>
    /// <param name="entityId">Entity Id</param>
    /// <param name="entity">Entity</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    public async Task UpdateEntityAsync(int entityId, TEntity entity,  CancellationToken cancellationToken)
    {
        var model = _mapper.MapToModel(entity);
        await UpdateModelAsync(entityId, model, cancellationToken);
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
        var dbSet = GetDbSet();
        if (dbSet != null)
        {
            var entity = await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity != null)
            {
                dbSet.Remove(entity);
            }
        }
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
        return dbSet != null ? await dbSet.ToListAsync(cancellationToken) : Enumerable.Empty<TModel>();
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
        return dbSet != null ? await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) : null;
    }

    /// <summary>
    /// Asynchronously create a new model of a specific type.
    /// </summary>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TModel"/> values.</returns>
    [ExcludeFromCodeCoverage]
    private async Task CreateModelAsync(TModel model, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        if (dbSet != null)
        {
            await dbSet.AddAsync(model, cancellationToken);
        }
    }

    /// <summary>
    /// Asynchronously update an existing model of a specific type.
    /// </summary>
    /// <param name="id">Model Id</param>
    /// <param name="model">Model</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>Result indicating success or failure of the operation with the <typeparamref name="TEntity"/> values.</returns>
    [ExcludeFromCodeCoverage]
    public async Task UpdateModelAsync(int id, TModel model, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        if (dbSet != null)
        {
            var existingModel = await dbSet.FindAsync([id], cancellationToken);
            if (existingModel != null)
            {
                existingModel.UpdateFrom(model);
            }
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
    private DbSet<TModel>? GetDbSet()
    {
        var property = _context.GetType()
            .GetProperties()
            .First(p => p.PropertyType == typeof(DbSet<TModel>));

        CheckPropertyValidity(property);

        var propertyInfo = property.GetValue(_context);
        return propertyInfo != null ? (DbSet<TModel>)propertyInfo : null;
    }

    /// <summary>
    /// Checks the validity of the property retrieved from the database context. 
    /// </summary>
    /// <remarks>
    /// If the property is null, an exception is thrown indicating that no DbSet for the specified model type was found in the context.
    /// </remarks>
    /// <param name="property"></param>
    /// <exception cref="InvalidOperationException"></exception>
    [ExcludeFromCodeCoverage]
    private void CheckPropertyValidity(PropertyInfo property)
    {
        if (property == null)
            throw new InvalidOperationException($"No DbSet<{typeof(TModel).Name}> found in {_context.GetType().Name}");
    }
}
