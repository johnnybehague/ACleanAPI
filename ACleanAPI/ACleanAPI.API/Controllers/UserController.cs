using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Application.Users.Queries.GetUserById;
using ACleanAPI.Application.Users.Queries.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Index(CancellationToken cancellationToken)
    {
        var query = new GetUsersQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        return Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> Details(int id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Id = id };
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsFailed)
            return BadRequest();

        var user = result.Value;
        if (user == null)
            return NotFound();

        return Ok(user);
    }
}
