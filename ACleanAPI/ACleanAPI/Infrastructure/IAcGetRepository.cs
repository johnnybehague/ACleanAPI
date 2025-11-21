using ACleanAPI.Domain;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq.Expressions;

namespace ACleanAPI.Infrastructure;

public interface IAcRepository
{
}

//public interface IAcGetRepository<TModel, TEntity>
//    where TModel : IAcModel
//    where TEntity : IAcEntity
//{
//    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
//    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
//    //Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);
//    //Task<TEntity> GetEntityByConditionAsync(Expression<Func<TModel, bool>> condition, CancellationToken cancellationToken);
//}

public interface IAcGetAllRepository<TEntity>
    where TEntity : IAcEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    // Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
}

public interface IAcGetDetailRepository<TEntity>
    where TEntity : IAcEntity
{
    Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
    // Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
}