using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.QueryHandlers;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.Common;
using FluentResults;

namespace ACleanAPI.Tests.App.Application;

public class GetUserByIdTestQueryHandler : AcGetEntityByIdQueryHandlerBase<UserTestEntity, UserTestDto>
{
    public GetUserByIdTestQueryHandler(IAcEntityRepository<UserTestEntity> repository, IAcEntityMapper<UserTestEntity, UserTestDto> mapper)
    : base(repository, mapper)
    {
    }

    public async Task<Result<UserTestDto?>> Handle(IAcGetEntityByIdRequest<UserTestDto> request, CancellationToken cancellationToken)
        => await HandleRequest(request, cancellationToken);
}
