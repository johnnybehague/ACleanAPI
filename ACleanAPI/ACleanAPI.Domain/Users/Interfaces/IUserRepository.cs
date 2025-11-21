using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Infrastructure;

namespace ACleanAPI.Domain.Users.Interfaces;

public interface IUserRepository : IAcGetAllRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    // Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
}

public interface IUserDetailRepository : IAcGetDetailRepository<User>
{
    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
}

