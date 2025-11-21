using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Application.Users.Mappers;
using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : AcGetEntitiesQueryHandlerBase<User, UserDto>, 
    IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>> 
{
    public GetUsersQueryHandler(IUserRepository userRepository,IUserMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        => await this.HandleRequest(request, cancellationToken);
}
