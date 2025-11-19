using System.ComponentModel.DataAnnotations.Schema;

namespace ACleanAPI.Infrastructure.Models;

[Table("Users")]
public class UserModel
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
