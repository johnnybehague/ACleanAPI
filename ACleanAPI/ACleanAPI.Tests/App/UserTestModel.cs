using ACleanAPI.Domain.Interfaces;
using ACleanAPI.Infrastructure.Core;

namespace ACleanAPI.Tests.App;

public class UserTestModel : AcModelBase, IAcModel
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
