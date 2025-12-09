using ACleanAPI.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACleanAPI.Example.Infrastructure.Models;

[Table("Users")]
public class UserModel : AcModelBase
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
