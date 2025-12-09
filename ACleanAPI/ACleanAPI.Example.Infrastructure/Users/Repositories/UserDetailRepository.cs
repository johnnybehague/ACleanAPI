using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Example.Infrastructure.Persistence;
using ACleanAPI.Example.Infrastructure.Users.Mappers;
using ACleanAPI.Infrastructure.Repositories;

namespace ACleanAPI.Example.Infrastructure.Users.Repositories;

public class UserDetailRepository : AcGetEntityByIdRepositoryBase<UserModel, User>,
    IUserDetailRepository
{
    public UserDetailRepository(AppDbContext context, IUserModelMapper mapper)
    : base(context, mapper)
    {
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        => await GetByIdAsync(id, cancellationToken);
}
