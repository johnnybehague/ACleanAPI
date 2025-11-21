using ACleanAPI.Application.Users.DTO;
using FluentResults;
using MediatR;

namespace ACleanAPI.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery : IAcEntityRequest<UserDetailDto>
{
    public int Id { get; set; }
}
