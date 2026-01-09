using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Requests;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Commands;

public record DeleteUserCommand(int? Id) : AcDeleteEntityRequest(Id);

public class DeleteUserCommandHandler : AcDeleteEntityCommandHandlerBase<User>, IRequestHandler<DeleteUserCommand, Result>
{
    public DeleteUserCommandHandler(IUserRepository userRepository)
        : base(userRepository)
    {
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
     => await HandleRequest(request, cancellationToken);
}
