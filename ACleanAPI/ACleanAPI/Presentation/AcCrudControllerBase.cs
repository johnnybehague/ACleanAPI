using ACleanAPI.Application.Commands;
using ACleanAPI.Application.Core;
using ACleanAPI.Application.Queries;
using ACleanAPI.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.Presentation;

/// <summary>
///  Base class for CRUD Controller.
/// </summary>
/// <remarks>This class provide GetAll, GetById, Create, Update and Delete methods</remarks>
public abstract class AcCrudControllerBase : ControllerBase
{
    /// <summary>
    /// Mediator for executing query and command operations within the application.
    /// </summary>
    protected readonly IAcMediator _mediator;

    /// <summary>
    ///  Initializes a new instance of the <see cref="AcCrudControllerBase"/> class with the specified mediator.
    /// </summary>
    /// <param name="mediator">Mediator for executing query and command operations</param>
    protected AcCrudControllerBase(IAcMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Asynchronously get all data.
    /// </summary>
    /// <typeparam name="TDto">DTO</typeparam>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>IEnumerable of data</returns>
    public async Task<ActionResult<IEnumerable<TDto>>> GetAllAsync<TDto>(AcGetEntitiesQuery<TDto> request, CancellationToken cancellationToken = default)
        where TDto : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.QueryAsync(request, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        return Ok(result.Value);
    }

    /// <summary>
    /// Asynchronously get single data.
    /// </summary>
    /// <typeparam name="TDto">DTO</typeparam>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>ActionResult of data</returns>
    public async Task<ActionResult<TDto>> GetByIdAsync<TDto>(AcGetEntityByIdQuery<TDto> request, CancellationToken cancellationToken = default)
        where TDto : AcEntityDtoBase
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

    /// <summary>
    /// Asynchronously create data.
    /// </summary>
    /// <typeparam name="TDto">DTO</typeparam>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>IActionResult</returns>
    public async Task<IActionResult> CreateAsync<TDto>(AcCreateEntityCommand<TDto> request, CancellationToken cancellationToken = default)
        where TDto : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.SendAsync(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    /// <summary>
    /// Asynchronously update specified data.
    /// </summary>
    /// <typeparam name="TDto">DTO</typeparam>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>IActionResult</returns>
    public async Task<IActionResult> UpdateAsync<TDto>(AcUpdateEntityCommand<TDto> request, CancellationToken cancellationToken = default)
        where TDto : AcEntityDtoBase
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.SendAsync(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }

    /// <summary>
    /// Asynchronously delete specified data.
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>IActionResult</returns>
    public async Task<IActionResult> DeleteAsync(AcDeleteEntityCommand request, CancellationToken cancellationToken = default)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _mediator.SendAsync(request, cancellationToken);
        if (result.IsFailed)
            return BadRequest();

        return NoContent();
    }
}
