using ACleanAPI.Domain.Core;
using ACleanAPI.Domain.Interfaces;

namespace ACleanAPI.Example.Domain.Users.Entities;

public class User : AcEntityBase, IAcEntity
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
