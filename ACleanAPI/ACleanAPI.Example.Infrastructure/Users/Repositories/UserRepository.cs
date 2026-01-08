using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Example.Infrastructure.Persistence;
using ACleanAPI.Example.Infrastructure.Users.Mappers;
using ACleanAPI.Infrastructure.Repositories;

namespace ACleanAPI.Example.Infrastructure.Users.Repositories;

public class UserRepository : AcEntityRepositoryBase<UserModel, User>,
    IUserRepository
{
    public UserRepository(AppDbContext context, IUserModelMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        => await GetEntitiesAsync(cancellationToken);

    public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await GetEntityByIdAsync(id, cancellationToken);

    public async Task CreateAsync(User entity, CancellationToken cancellationToken)
        => await CreateEntityAsync(entity, cancellationToken);

    public async Task UpdateAsync(int id, User entity, CancellationToken cancellationToken)
        => await UpdateEntityAsync(id,  entity, cancellationToken);

    public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        => await DeleteEntityAsync(id, cancellationToken);
}
