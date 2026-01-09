using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.Common;
using FluentResults;

namespace ACleanAPI.Tests.App.Application;

public class UpdateUserTestCommandHandler : AcUpdateEntityCommandHandlerBase<UserTestDto, UserTestEntity>
{
    public UpdateUserTestCommandHandler(IAcEntityRepository<UserTestEntity> repository, IAcEntityMapper<UserTestEntity, UserTestDto> mapper)
    : base(repository, mapper)
    {
    }

    public async Task<Result> Handle(AcUpdateEntityRequest<UserTestDto> request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
