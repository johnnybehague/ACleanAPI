using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Commands;

public record CreateUserCommand : IAcCreateEntityRequest<UserDto>
{
    public UserDto? Dto { get; set; }
}

public class CreateUserCommandHandler : AcCreateEntityCommandHandlerBase<UserDto, User>, IRequestHandler<CreateUserCommand, Result>
{
    public CreateUserCommandHandler(IUserRepository userRepository, IUserMapper userMapper)
    : base(userRepository, userMapper)
    {
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
     => await HandleRequest(request, cancellationToken);
}
