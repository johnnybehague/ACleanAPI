using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Application.Users.Mappers;
using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : AcGetEntityQueryHandlerBase<User, UserDetailDto>, 
    IRequestHandler<GetUserByIdQuery, Result<UserDetailDto>>
{
    public GetUserByIdQueryHandler(IUserDetailRepository userRepository, IUserDetailMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result<UserDetailDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
     => await this.HandleRequest(request, cancellationToken);
}

