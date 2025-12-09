using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : AcGetEntityByIdQueryHandlerBase<User, UserDetailDto>, 
    IRequestHandler<GetUserByIdQuery, Result<UserDetailDto>>
{
    public GetUserByIdQueryHandler(IUserDetailRepository userRepository, IUserDetailMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result<UserDetailDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
     => await HandleRequest(request, cancellationToken);
}

