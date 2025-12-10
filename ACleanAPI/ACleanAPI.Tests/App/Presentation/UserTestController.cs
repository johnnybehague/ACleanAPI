using ACleanAPI.Application.Interfaces;
using ACleanAPI.Presentation;
using ACleanAPI.Tests.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Tests.App.Presentation;

public class UserTestController : AcGetControllerBase<UserTestDto, UserTestDetailDto>
{
    public UserTestController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<ActionResult<IEnumerable<UserTestDto>>> Index(IAcGetEntitiesRequest<UserTestDto> request, CancellationToken cancellationToken = default)
        => await GetEntitiesAsync(request, cancellationToken);

    public async Task<ActionResult<UserTestDetailDto>> Details(IAcGetEntityByIdRequest<UserTestDetailDto> request, CancellationToken cancellationToken = default)
        => await GetEntityAsync(request, cancellationToken);
}
