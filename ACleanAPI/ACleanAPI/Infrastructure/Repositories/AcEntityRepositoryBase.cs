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

    public AcEntityRepositoryBase(DbContext context, IAcModelMapper<TModel, TEntity> mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        var data = await GetDbSet().ToListAsync(cancellationToken);
        return data.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
    {
        var data = await GetDbSet().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        return _mapper.MapToEntity(data);
    }

    private DbSet<TModel> GetDbSet()
    {
        var property = _context.GetType()
            .GetProperties()
            .FirstOrDefault(p => p.PropertyType == typeof(DbSet<TModel>));

        CheckPropertyValidity(property);

        return (DbSet<TModel>)property.GetValue(_context);
    }

    [ExcludeFromCodeCoverage]
    private void CheckPropertyValidity(PropertyInfo property)
    {
        if (property == null)
            throw new InvalidOperationException($"No DbSet<{typeof(TModel).Name}> found in {_context.GetType().Name}");
    }
}
