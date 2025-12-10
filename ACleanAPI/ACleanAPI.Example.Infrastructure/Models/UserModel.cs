using ACleanAPI.Infrastructure.Core;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACleanAPI.Example.Infrastructure.Models;

[Table("Users")]
public class UserModel : AcModelBase
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
