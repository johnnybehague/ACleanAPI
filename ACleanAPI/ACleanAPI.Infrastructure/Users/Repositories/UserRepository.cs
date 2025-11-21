using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Domain.Users.Interfaces;
using ACleanAPI.Infrastructure.Models;
using ACleanAPI.Infrastructure.Persistence;
using ACleanAPI.Infrastructure.Users.Mappers;

namespace ACleanAPI.Infrastructure.Users.Repositories;

public class UserRepository : AcGetEntitiesRepositoryBase<UserModel, User>,
    IUserRepository
{
    public UserRepository(AppDbContext context, IUserModelMapper mapper)
        : base(context, mapper)
    {
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        => await GetEntitiesAsync(cancellationToken);
}
