using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Infrastructure.Interfaces;

namespace ACleanAPI.Example.Infrastructure.Users.Mappers;

public interface IUserModelMapper : IAcModelMapper<UserModel, User>;

public class UserModelMapper : IUserModelMapper
{
    public User MapToEntity(UserModel model)
    {
        return new User
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email
        };
    }
}
