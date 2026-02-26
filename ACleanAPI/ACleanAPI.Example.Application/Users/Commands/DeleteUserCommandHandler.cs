using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Commands;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using LiteBus.Commands.Abstractions;

namespace ACleanAPI.Example.Application.Users.Commands;

public record DeleteUserCommand(int? Id) : AcDeleteEntityCommand(Id);

public class DeleteUserCommandHandler : AcDeleteEntityCommandHandlerBase<User>, ICommandHandler<DeleteUserCommand, Result>
{
    public DeleteUserCommandHandler(IUserRepository userRepository)
        : base(userRepository)
    {
    }

    public async Task<Result> HandleAsync(DeleteUserCommand request, CancellationToken cancellationToken)
     => await HandleRequest(request, cancellationToken);
}
