using ACleanAPI.Application.Core;
using ACleanAPI.Application.Interfaces;

namespace ACleanAPI.Example.Application.Users.DTO;

public class UserDto : AcEntityDtoBase, IAcEntityDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
