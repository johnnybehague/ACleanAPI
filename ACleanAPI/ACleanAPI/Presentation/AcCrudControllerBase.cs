using ACleanAPI.Application.Core;
using ACleanAPI.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Presentation;

public abstract class AcCrudControllerBase<Dto, DetailDto> : ControllerBase
    where Dto : AcEntityDtoBase
    where DetailDto : AcEntityDtoBase
{
    protected readonly IMediator _mediator;

    protected AcCrudControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ActionResult<IEnumerable<Dto>>> GetEntitiesAsync(AcGetEntitiesRequest<Dto> request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        return Ok(result.Value);
    }

    public async Task<ActionResult<DetailDto>> GetEntityByIdAsync(AcGetEntityByIdRequest<DetailDto> request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
        {
            if(result.Errors.Any(e => e.Message == "ENTITY_NOT_FOUND"))
                return NotFound();

            return BadRequest();
        }

        return Ok(result.Value);
    }

    public async Task<IActionResult> CreateEntityAsync(AcCreateEntityRequest<Dto> request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    public async Task<IActionResult> UpdateEntityAsync(AcUpdateEntityRequest<Dto> request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    public async Task<IActionResult> DeleteEntityAsync(AcDeleteEntityRequest request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }
}
