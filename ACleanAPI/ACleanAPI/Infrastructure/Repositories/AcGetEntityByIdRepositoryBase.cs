using ACleanAPI.Domain;
using ACleanAPI.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ACleanAPI.Infrastructure.Repositories;

public class AcGetEntityByIdRepositoryBase<TModel, TEntity> : IAcGetEntityByIdRepository<TEntity>
    where TModel : AcModelBase
    where TEntity : AcEntityBase
{
    private readonly DbContext _context;

    private IAcModelMapper<TModel, TEntity> _mapper { get; }

    public AcGetEntityByIdRepositoryBase(DbContext context, IAcModelMapper<TModel, TEntity> mapper)
    {
        _context = context;
        _mapper = mapper;
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

        if (property == null)
            throw new InvalidOperationException($"No DbSet<{typeof(TModel).Name}> found in {_context.GetType().Name}");

        return (DbSet<TModel>)property.GetValue(_context);
    }
}

