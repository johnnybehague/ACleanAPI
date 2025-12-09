using ACleanAPI.Application;

namespace ACleanAPI.Example.Application.Users.DTO;

public class UserDetailDto : AcEntityDtoBase
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
