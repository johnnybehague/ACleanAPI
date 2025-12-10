using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public sealed class UserMapperTests
{
    private UserMapper _mapper;

    [TestInitialize]
    public void Setup()
    {
        _mapper = new UserMapper();
    }

    [TestMethod]
    public void MapToDto_ThrowsArgumentNullException_WhenUserIsNull()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => _mapper.MapToDto(null));
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
}