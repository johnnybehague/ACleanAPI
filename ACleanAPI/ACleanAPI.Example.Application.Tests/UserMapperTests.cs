using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public sealed class UserMapperTests
{
    private UserMapper _mapper;

    public UserMapperTests()
    {
        _mapper = new UserMapper();
    }

    [TestMethod]
    public void MapToDto_MapsPropertiesCorrectly()
    {
        // Arrange
        var user = new User
        {
            Id = 1,
            FirstName = "Alice",
            LastName = "Dupont"
        };

        // Act
        var dto = _mapper.MapToDto(user);

        // Assert
        Assert.IsNotNull(dto);
        Assert.AreEqual(user.Id, dto.Id);
        Assert.AreEqual(user.FirstName, dto.FirstName);
        Assert.AreEqual(user.LastName, dto.LastName);
    }

    [TestMethod]
    public void MapToEntity_MapsPropertiesCorrectly()
    {
        // Arrange
        var dto = new UserDto
        {
            Id = 1,
            FirstName = "Alice",
            LastName = "Dupont"
        };

        // Act
        var entity = _mapper.MapToEntity(dto);

        // Assert
        Assert.IsNotNull(dto);
        Assert.AreEqual(dto.Id, entity.Id);
        Assert.AreEqual(dto.FirstName, entity.FirstName);
        Assert.AreEqual(dto.LastName, entity.LastName);
    }
}
