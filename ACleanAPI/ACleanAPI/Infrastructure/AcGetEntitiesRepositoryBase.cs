using ACleanAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace ACleanAPI.Infrastructure;

public class AcGetEntitiesRepositoryBase<TModel, TEntity> : IAcGetEntitiesRepository<TEntity>
    where TModel : AcModelBase
    where TEntity : AcEntityBase
{
    private readonly DbContext _context;

    private IAcModelMapper<TModel, TEntity> _mapper { get; }

    public AcGetEntitiesRepositoryBase(DbContext context, IAcModelMapper<TModel, TEntity> mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
    {
        var data = await GetDbSet().ToListAsync(cancellationToken);
        return data.Select(_mapper.MapToEntity).ToList();
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
