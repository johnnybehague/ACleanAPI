using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Commands;

public record UpdateUserCommand(int Id, UserDto? Dto) : AcUpdateEntityRequest<UserDto>(Id, Dto);

public class UpdateUserCommandHandler : AcUpdateEntityCommandHandlerBase<UserDto, User>, IRequestHandler<UpdateUserCommand, Result>
{
    public UpdateUserCommandHandler(IUserRepository userRepository, IUserMapper userMapper)
        : base(userRepository, userMapper)
    {
    }

    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
