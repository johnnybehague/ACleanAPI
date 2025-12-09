using ACleanAPI.Application;
using ACleanAPI.Example.Application.Users.DTO;

namespace ACleanAPI.Example.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IAcGetEntitiesRequest<UserDto>; //, IRequest<Result<IEnumerable<UserDto>>>; // IRequest<Result<IEnumerable<UserDto>>>;
