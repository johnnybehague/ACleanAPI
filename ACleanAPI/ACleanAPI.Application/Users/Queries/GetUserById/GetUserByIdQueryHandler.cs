using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Application.Users.Mappers;
using ACleanAPI.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDetailDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<Result<UserDetailDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        var dto = _userMapper.MapToDetailDto(user);
        return Result.Ok(dto);
    }
}
