using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Application.Users.Queries.GetUserById;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;

namespace ACleanAPI.Example.Application.Users.Commands;

public record DeleteUserCommand : IAcDeleteEntityRequest
{
    public int? Id { get; set; }
}

public class DeleteUserCommandHandler : AcDeleteEntityCommandHandlerBase<User>, IRequestHandler<DeleteUserCommand, Result>
{
    public DeleteUserCommandHandler(IUserRepository userRepository)
        : base(userRepository)
    {
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
     => await HandleRequest(request, cancellationToken);
}
