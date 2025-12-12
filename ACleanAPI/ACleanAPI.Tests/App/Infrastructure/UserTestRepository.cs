using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Infrastructure.Repositories;
using ACleanAPI.Tests.Common;
using Microsoft.EntityFrameworkCore;

namespace ACleanAPI.Tests.App.Infrastructure;

public class UserTestRepository : AcEntityRepositoryBase<UserTestModel, UserTestEntity>
{
    public UserTestRepository(DbContext context, IAcModelMapper<UserTestModel, UserTestEntity> mapper) : base(context, mapper)
    {
    }

    public Task<IEnumerable<UserTestEntity>> GetUsersAsync(CancellationToken cancellationToken)
        => GetEntitiesAsync(cancellationToken);

    public Task<UserTestEntity?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        => GetEntityByIdAsync(id, cancellationToken);
}
