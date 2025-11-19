using ACleanAPI.Domain.Users.Entities;

namespace ACleanAPI.Domain.Users.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);

    Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
}
