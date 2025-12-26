using ACleanAPI.Application.Interfaces;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Domain.Users.Entities;

namespace ACleanAPI.Example.Application.Users.Mappers;

public interface IUserMapper : IAcEntityMapper<User, UserDto>
{
}

public class UserMapper : IUserMapper
{
    public UserDto MapToDto(User user)
    {
        ArgumentNullException.ThrowIfNull(user);

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    public User MapToEntity(UserDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        return new User
        {
            Id = dto.Id,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };
    }
}
