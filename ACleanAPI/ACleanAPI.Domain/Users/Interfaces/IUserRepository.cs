using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Infrastructure;

namespace ACleanAPI.Domain.Users.Interfaces;

public interface IUserRepository : IAcGetEntitiesRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    //Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
}

public interface IUserDetailRepository : IAcGetEntityByIdRepository<User>
{
    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
}

