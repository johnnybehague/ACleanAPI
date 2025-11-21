using ACleanAPI.Application.Users.DTO;
using ACleanAPI.Domain.Users.Entities;

namespace ACleanAPI.Application.Users.Mappers;

public interface IUserMapper : IAcEntityMapper<User, UserDto>
{
    //UserDto MapToDto(User user);

    //UserDetailDto MapToDetailDto(User user);
}

public class UserMapper : IUserMapper // IUserMapper
{
    public UserDto MapToDto(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }

    //public UserDetailDto MapToDetailDto(User user)
    //{
    //    if (user == null)
    //        throw new ArgumentNullException(nameof(user));

    //    return new UserDetailDto
    //    {
    //        Id = user.Id,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        Email = user.Email
    //    };
    //}
}

public interface IUserDetailMapper : IAcEntityMapper<User, UserDetailDto>
{
    //UserDto MapToDto(User user);

    //UserDetailDto MapToDetailDto(User user);
}

public class UserDetailMapper : IUserDetailMapper // IUserMapper
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

    //public UserDetailDto MapToDetailDto(User user)
    //{
    //    if (user == null)
    //        throw new ArgumentNullException(nameof(user));

    //    return new UserDetailDto
    //    {
    //        Id = user.Id,
    //        FirstName = user.FirstName,
    //        LastName = user.LastName,
    //        Email = user.Email
    //    };
    //}
}