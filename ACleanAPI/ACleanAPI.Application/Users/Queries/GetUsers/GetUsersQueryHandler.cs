using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Application.Users.Mappers;
using ACleanAPI.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserMapper _userMapper;

    public GetUsersQueryHandler(IUserRepository userRepository, IUserMapper userMapper)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
    }

    public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(cancellationToken);
        IEnumerable<UserDto> dtos = users.Select(x => _userMapper.MapToDto(x)).ToList();
        return Result.Ok(dtos);
    }
}
