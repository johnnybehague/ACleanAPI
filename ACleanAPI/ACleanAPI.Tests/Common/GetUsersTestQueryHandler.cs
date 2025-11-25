using ACleanAPI.Application;
using ACleanAPI.Infrastructure;
using FluentResults;
using MediatR;

namespace ACleanAPI.Tests.Common;

public class GetUsersTestQueryHandler : AcGetEntitiesQueryHandlerBase<UserTestEntity, UserTestDto>
{
    public GetUsersTestQueryHandler(IAcGetEntitiesRepository<UserTestEntity> repository, IAcEntityMapper<UserTestEntity, UserTestDto> mapper)
        : base(repository, mapper)
    {
    }

    public async Task<Result<IEnumerable<UserTestDto>>> Handle(IAcGetEntitiesRequest<UserTestDto> request, CancellationToken cancellationToken)
        => await this.HandleRequest(request, cancellationToken);
}