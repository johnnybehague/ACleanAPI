using ACleanAPI.Application;
using ACleanAPI.Example.Application.Users.DTO;

namespace ACleanAPI.Example.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery : IAcGetEntityByIdRequest<UserDetailDto>
{
    public int Id { get; set; }
}
