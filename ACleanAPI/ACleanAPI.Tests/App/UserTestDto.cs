using ACleanAPI.Application;
using ACleanAPI.Application.Interfaces;

namespace ACleanAPI.Tests.Common;

public class UserTestDto : AcEntityDtoBase, IAcEntityDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
