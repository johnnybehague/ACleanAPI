using ACleanAPI.Domain.Core;
using ACleanAPI.Infrastructure.Core;
using ACleanAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ACleanAPI.Infrastructure.Repositories;

public abstract class AcEntityRepositoryBase<TModel, TEntity> : IAcEntityRepository<TEntity>
    where TModel : AcModelBase
    where TEntity : AcEntityBase
{
    private readonly DbContext _context;

    private IAcModelMapper<TModel, TEntity> _mapper { get; }

    protected AcEntityRepositoryBase(DbContext context, IAcModelMapper<TModel, TEntity> mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        var data = await GetModelsAsync(cancellationToken);
        return data.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<TEntity?> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await GetModelByIdAsync(id, cancellationToken);
        return  data != null ? _mapper.MapToEntity(data) : null;
    }

    [ExcludeFromCodeCoverage]
    private async Task<IEnumerable<TModel>> GetModelsAsync(CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();

        return dbSet != null ? await dbSet.ToListAsync(cancellationToken) : Enumerable.Empty<TModel>();
    }

    [ExcludeFromCodeCoverage]
    private async Task<TModel?> GetModelByIdAsync(int id, CancellationToken cancellationToken)
    {
        var dbSet = GetDbSet();
        return dbSet != null ? await dbSet.FirstOrDefaultAsync(x => x.Id == id, cancellationToken) : null;
    }

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

    [ExcludeFromCodeCoverage]
    private void CheckPropertyValidity(PropertyInfo property)
    {
        if (property == null)
            throw new InvalidOperationException($"No DbSet<{typeof(TModel).Name}> found in {_context.GetType().Name}");
    }
}
