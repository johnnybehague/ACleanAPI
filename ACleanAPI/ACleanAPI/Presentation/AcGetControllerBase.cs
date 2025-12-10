using ACleanAPI.Application.Core;
using ACleanAPI.Application.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Presentation;

public abstract class AcGetControllerBase<Dto, DetailDto> : ControllerBase
    where Dto : AcEntityDtoBase
    where DetailDto : AcEntityDtoBase
{
    protected readonly IMediator _mediator;

    public AcGetControllerBase(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ActionResult<IEnumerable<Dto>>> GetEntitiesAsync(IAcGetEntitiesRequest<Dto> request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        return Ok(result.Value);
    }

    public async Task<ActionResult<DetailDto>> GetEntityAsync(IRequest<Result<DetailDto>> request, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return result.Value is not null ? Ok(result.Value) : NotFound();
    }
}
