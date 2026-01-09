using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Infrastructure.Interfaces;

namespace ACleanAPI.Example.Domain.Users.Interfaces;

public interface IUserRepository : IAcEntityRepository<User>
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);
}

