using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Requests;
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

    public async Task<Result> Handle(AcDeleteEntityRequest request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
