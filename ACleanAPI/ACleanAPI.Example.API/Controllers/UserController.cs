using ACleanAPI.Example.Application.Users.Commands;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Queries;
using ACleanAPI.Presentation;
using ACleanAPI.Presentation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ACleanAPI.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : AcCrudControllerBase
{
    public UserController(IAcMediator mediator) : base(mediator) { }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Index(CancellationToken cancellationToken)
        => await GetAllAsync(new GetUsersQuery(), cancellationToken);

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto>> Details(int id, CancellationToken cancellationToken)
        => await GetByIdAsync(new GetUserByIdQuery(id), cancellationToken);

    [HttpPost]
    public async Task<IActionResult> Create(UserDto dto, CancellationToken cancellationToken)
        => await CreateAsync(new CreateUserCommand(dto), cancellationToken);

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserDto dto, CancellationToken cancellationToken)
        => await UpdateAsync(new UpdateUserCommand(id, dto), cancellationToken);

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        => await DeleteAsync(new DeleteUserCommand(id), cancellationToken);
}
