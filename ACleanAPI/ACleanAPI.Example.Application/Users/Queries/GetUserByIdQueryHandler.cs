using ACleanAPI.Application.Queries;
using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using LiteBus.Queries.Abstractions;

namespace ACleanAPI.Example.Application.Users.Queries;

public record GetUserByIdQuery(int? Id) : AcGetEntityByIdQuery<UserDetailDto>(Id);

public class GetUserByIdQueryHandler : AcGetEntityByIdQueryHandlerBase<User, UserDetailDto>, 
    IQueryHandler<GetUserByIdQuery, Result<UserDetailDto>>
{
    public GetUserByIdQueryHandler(IUserRepository userRepository, IUserDetailMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result<UserDetailDto>> HandleAsync(GetUserByIdQuery request, CancellationToken cancellationToken = default)
     => await HandleRequest(request, cancellationToken);
}

