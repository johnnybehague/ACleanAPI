using ACleanAPI.Application.Users.DTO;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Users.Queries.GetUsers;

public record GetUsersQuery : IAcEntitiesRequest<UserDto>; //, IRequest<Result<IEnumerable<UserDto>>>; // IRequest<Result<IEnumerable<UserDto>>>;
