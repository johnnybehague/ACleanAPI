using ACleanAPI.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ACleanAPI.Infrastructure;

public class AcGetRepositoryBase<TModel, TEntity> : IAcRepository
    where TModel : AcModelBase
    where TEntity : AcEntityBase
{
    private readonly DbContext _context;

    private IAcModelMapper<TModel, TEntity> _mapper { get; }

    public AcGetRepositoryBase(DbContext context, IAcModelMapper<TModel, TEntity> mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        var data = await GetDbSet().ToListAsync(cancellationToken);
        return data.Select(_mapper.MapToEntity).ToList();
    }

    public async Task<TEntity> GetEntityByConditionAsync(Expression<Func<TModel, bool>> condition, CancellationToken cancellationToken)
    {
        var data = await GetDbSet().FirstOrDefaultAsync(condition, cancellationToken);
        return _mapper.MapToEntity(data);
    }

    private DbSet<TModel> GetDbSet()
    {
        var property = _context.GetType()
            .GetProperties()
            .FirstOrDefault(p => p.PropertyType == typeof(DbSet<TModel>));

        if (property == null)
            throw new InvalidOperationException($"No DbSet<{typeof(TModel).Name}> found in {_context.GetType().Name}");

        return (DbSet<TModel>)property.GetValue(_context);
    }
}

