using ACleanAPI.Application.Core;
using ACleanAPI.Application.Interfaces;

namespace ACleanAPI.Tests.Common;

public class UserTestDto : AcEntityDtoBase, IAcEntityDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
