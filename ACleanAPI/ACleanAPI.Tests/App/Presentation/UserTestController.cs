using ACleanAPI.Application.Requests;
using ACleanAPI.Presentation;
using ACleanAPI.Tests.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Tests.App.Presentation;

[ApiController]
[Route("api/[controller]")]
public class UserTestController : AcCrudControllerBase
{
    public UserTestController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<ActionResult<IEnumerable<UserTestDto>>> Index(AcGetEntitiesRequest<UserTestDto> request, CancellationToken cancellationToken = default)
        => await GetEntitiesAsync(request, cancellationToken);

    public async Task<ActionResult<UserTestDetailDto>> Details(AcGetEntityByIdRequest<UserTestDetailDto> request, CancellationToken cancellationToken = default)
        => await GetEntityByIdAsync(request, cancellationToken);
}
