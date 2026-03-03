using ACleanAPI.Application.Queries;
using ACleanAPI.Presentation;
using ACleanAPI.Presentation.Interfaces;
using ACleanAPI.Tests.Common;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Tests.App.Presentation;

[ApiController]
[Route("api/[controller]")]
public class UserTestController : AcCrudControllerBase
{
    public UserTestController(IAcMediator mediator) : base(mediator)
    {
    }

    public async Task<ActionResult<IEnumerable<UserTestDto>>> Index(AcGetEntitiesQuery<UserTestDto> request, CancellationToken cancellationToken = default)
        => await GetAllAsync(request, cancellationToken);

    public async Task<ActionResult<UserTestDetailDto>> Details(AcGetEntityByIdQuery<UserTestDetailDto> request, CancellationToken cancellationToken = default)
        => await GetByIdAsync(request, cancellationToken);
}
