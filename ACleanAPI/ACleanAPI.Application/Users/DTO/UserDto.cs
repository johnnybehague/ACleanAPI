namespace ACleanAPI.Application.Users.DTO;

public class UserDto : AcEntityDtoBase, IAcEntityDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
