using ACleanAPI.Application.Interfaces;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Domain.Users.Entities;

namespace ACleanAPI.Example.Application.Users.Mappers;

public interface IUserDetailMapper : IAcEntityMapper<User, UserDetailDto>
{
}

public class UserDetailMapper : IUserDetailMapper
{
    public UserDetailDto MapToDto(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return new UserDetailDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}