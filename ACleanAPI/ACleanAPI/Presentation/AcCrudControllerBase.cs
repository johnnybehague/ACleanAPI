using ACleanAPI.Application.Commands;
using ACleanAPI.Application.Core;
using ACleanAPI.Application.Queries;
using ACleanAPI.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Presentation;

public abstract class AcCrudControllerBase : ControllerBase
{
    protected readonly IAcMediator _mediator;

    protected AcCrudControllerBase(IAcMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ActionResult<IEnumerable<T>>> GetEntitiesAsync<T>(AcGetEntitiesQuery<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.QueryAsync(request, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        return Ok(result.Value);
    }

    public async Task<ActionResult<T>> GetEntityByIdAsync<T>(AcGetEntityByIdQuery<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.QueryAsync(request, cancellationToken);
        if (result.IsFailed)
        {
            if (result.Errors.Any(e => e.Message == "ENTITY_NOT_FOUND"))
                return NotFound();

            return BadRequest();
        }

        return Ok(result.Value);
    }

    public async Task<IActionResult> CreateEntityAsync<T>(AcCreateEntityCommand<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.SendAsync(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    public async Task<IActionResult> UpdateEntityAsync<T>(AcUpdateEntityCommand<T> request, CancellationToken cancellationToken = default)
        where T : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.SendAsync(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    public async Task<IActionResult> DeleteEntityAsync(AcDeleteEntityCommand request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.SendAsync(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }
}
