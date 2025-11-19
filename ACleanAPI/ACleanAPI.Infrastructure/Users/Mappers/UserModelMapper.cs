using ACleanAPI.Domain.Users.Entities;
using ACleanAPI.Infrastructure.Models;

namespace ACleanAPI.Infrastructure.Users.Mappers;

public interface IUserModelMapper
{
    User MapToEntity(UserModel model);
}

public class UserModelMapper : IUserModelMapper
{
    public User MapToEntity(UserModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        return new User
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };
    }
}
