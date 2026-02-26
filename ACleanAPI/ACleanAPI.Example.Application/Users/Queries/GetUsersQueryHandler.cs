using ACleanAPI.Application.Queries;
using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Example.Application.Users.Queries;
public record GetUsersQuery : AcGetEntitiesQuery<UserDto>;

public class GetUsersQueryHandler : AcGetEntitiesQueryHandlerBase<User, UserDto>, 
    IQueryHandler<GetUsersQuery, Result<IEnumerable<UserDto>>> 
{
    public GetUsersQueryHandler(IUserRepository userRepository,IUserMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result<IEnumerable<UserDto>>> HandleAsync(GetUsersQuery request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
