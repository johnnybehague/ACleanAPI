using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Commands;

public record UpdateUserCommand : IAcUpdateEntityRequest<UserDto>
{
    public int Id { get; set; }
    public UserDto? Dto { get; set; }
}

public class UpdateUserCommandHandler : AcUpdateEntityCommandHandlerBase<UserDto, User>, IRequestHandler<UpdateUserCommand, Result>
{
    public UpdateUserCommandHandler(IUserRepository userRepository, IUserMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
