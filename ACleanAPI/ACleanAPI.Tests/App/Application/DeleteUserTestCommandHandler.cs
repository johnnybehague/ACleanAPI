using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Commands;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.Common;
using FluentResults;

namespace ACleanAPI.Tests.App.Application;

public class DeleteUserTestCommandHandler : AcDeleteEntityCommandHandlerBase<UserTestEntity>
{
    public DeleteUserTestCommandHandler(IAcEntityRepository<UserTestEntity> repository)
    : base(repository)
    {
    }

    public async Task<Result> Handle(AcDeleteEntityCommand request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
