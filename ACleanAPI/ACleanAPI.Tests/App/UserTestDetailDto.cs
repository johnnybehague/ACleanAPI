using ACleanAPI.Application.Core;
using ACleanAPI.Application.Interfaces;

namespace ACleanAPI.Tests.App;

public class UserTestDetailDto : AcEntityDtoBase, IAcEntityDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}