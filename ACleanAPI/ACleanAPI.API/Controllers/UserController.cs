using ACleanAPI.Application;
using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Application.Users.Queries.GetUserById;
using ACleanAPI.Application.Users.Queries.GetUsers;
using ACleanAPI.Presentation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : AcGetControllerBase<UserDto, UserDetailDto>
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Index(CancellationToken cancellationToken)
        => await GetEntitiesAsync(new GetUsersQuery(), cancellationToken);

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto>> Details(int id, CancellationToken cancellationToken)
        => await GetEntityAsync(new GetUserByIdQuery { Id = id }, cancellationToken);
   
}
