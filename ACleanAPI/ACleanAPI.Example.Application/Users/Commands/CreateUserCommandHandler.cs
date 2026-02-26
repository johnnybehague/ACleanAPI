using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Commands;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using LiteBus.Commands.Abstractions;

namespace ACleanAPI.Example.Application.Users.Commands;

public record CreateUserCommand(UserDto? Dto) : AcCreateEntityCommand<UserDto>(Dto);

public class CreateUserCommandHandler : AcCreateEntityCommandHandlerBase<UserDto, User>, ICommandHandler<CreateUserCommand, Result>
{
    public CreateUserCommandHandler(IUserRepository userRepository, IUserMapper userMapper)
    : base(userRepository, userMapper)
    {
    }

    public async Task<Result> HandleAsync(CreateUserCommand request, CancellationToken cancellationToken)
     => await HandleRequest(request, cancellationToken);
}
