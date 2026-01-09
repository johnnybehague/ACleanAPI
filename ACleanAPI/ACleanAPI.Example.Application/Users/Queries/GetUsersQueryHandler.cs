using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Application.Requests;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Queries;
public record GetUsersQuery : AcGetEntitiesRequest<UserDto>;

public class GetUsersQueryHandler : AcGetEntitiesQueryHandlerBase<User, UserDto>, 
    IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>> 
{
    public GetUsersQueryHandler(IUserRepository userRepository,IUserMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
