using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Domain.Users.Interfaces;
using ACleanAPI.Infrastructure.Models;
using ACleanAPI.Infrastructure.Persistence;
using ACleanAPI.Infrastructure.Users.Mappers;

namespace ACleanAPI.Infrastructure.Users.Repositories;

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
