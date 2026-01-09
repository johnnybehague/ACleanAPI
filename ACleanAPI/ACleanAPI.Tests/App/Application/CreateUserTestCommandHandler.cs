using ACleanAPI.Application.CommandHandlers;
using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Requests;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.Common;
using FluentResults;

namespace ACleanAPI.Tests.App.Application;

public class CreateUserTestCommandHandler : AcCreateEntityCommandHandlerBase<UserTestDto, UserTestEntity>
{
    public CreateUserTestCommandHandler(IAcEntityRepository<UserTestEntity> repository, IAcEntityMapper<UserTestEntity, UserTestDto> mapper)
    : base(repository, mapper)
    {
    }

    public async Task<Result> Handle(AcCreateEntityRequest<UserTestDto> request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
