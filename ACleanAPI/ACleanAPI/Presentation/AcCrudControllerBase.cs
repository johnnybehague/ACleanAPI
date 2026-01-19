using ACleanAPI.Application.Core;
using ACleanAPI.Application.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Presentation;

public abstract class AcCrudControllerBase : ControllerBase
{
    protected readonly IMediator _mediator;

    protected AcCrudControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ActionResult<IEnumerable<T>>> GetEntitiesAsync<T>(AcGetEntitiesRequest<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        return Ok(result.Value);
    }

    public async Task<ActionResult<T>> GetEntityByIdAsync<T>(AcGetEntityByIdRequest<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
        {
            if (result.Errors.Any(e => e.Message == "ENTITY_NOT_FOUND"))
                return NotFound();

            return BadRequest();
        }

        return Ok(result.Value);
    }

    public async Task<IActionResult> CreateEntityAsync<T>(AcCreateEntityRequest<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    public async Task<IActionResult> UpdateEntityAsync<T>(AcUpdateEntityRequest<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
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
