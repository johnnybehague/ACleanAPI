using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Queries;
using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.Common;
using FluentResults;

namespace ACleanAPI.Tests.App.Application;

public class GetUsersTestQueryHandler : AcGetEntitiesQueryHandlerBase<UserTestEntity, UserTestDto>
{
    public GetUsersTestQueryHandler(IAcEntityRepository<UserTestEntity> repository, IAcEntityMapper<UserTestEntity, UserTestDto> mapper)
        : base(repository, mapper)
    {
    }

    public async Task<Result<IEnumerable<UserTestDto>>> Handle(AcGetEntitiesQuery<UserTestDto> request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}