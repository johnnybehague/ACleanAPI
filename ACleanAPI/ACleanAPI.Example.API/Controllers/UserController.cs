using ACleanAPI.Example.Application.Users.Commands;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Queries.GetUserById;
using ACleanAPI.Example.Application.Users.Queries.GetUsers;
using ACleanAPI.Presentation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : AcCrudControllerBase<UserDto, UserDetailDto>
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Index(CancellationToken cancellationToken)
        => await GetEntitiesAsync(new GetUsersQuery(), cancellationToken);

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto>> Details(int id, CancellationToken cancellationToken)
        => await GetEntityByIdAsync(new GetUserByIdQuery { Id = id }, cancellationToken);

    [HttpPost]
    public async Task<IActionResult> Create(UserDto dto, CancellationToken cancellationToken)
        => await CreateEntityAsync(new CreateUserCommand { Dto = dto }, cancellationToken);

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserDto dto, CancellationToken cancellationToken)
        => await UpdateEntityAsync(new UpdateUserCommand { Id = id, Dto = dto }, cancellationToken);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        => await DeleteEntityAsync(new DeleteUserCommand { Id = id }, cancellationToken);
}
