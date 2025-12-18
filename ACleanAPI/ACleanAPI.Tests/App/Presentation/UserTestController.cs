using ACleanAPI.Application.Interfaces;
using ACleanAPI.Presentation;
using ACleanAPI.Tests.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Tests.App.Presentation;

[ApiController]
[Route("api/[controller]")]
public class UserTestController : AcCrudControllerBase<UserTestDto, UserTestDetailDto>
{
    public UserTestController(IMediator mediator) : base(mediator)
    {
    }

    public async Task<ActionResult<IEnumerable<UserTestDto>>> Index(IAcGetEntitiesRequest<UserTestDto> request, CancellationToken cancellationToken = default)
        => await GetEntitiesAsync(request, cancellationToken);

    public async Task<ActionResult<UserTestDetailDto>> Details(IAcGetEntityByIdRequest<UserTestDetailDto> request, CancellationToken cancellationToken = default)
        => await GetEntityByIdAsync(request, cancellationToken);
}
