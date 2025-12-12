using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Example.Infrastructure.Users.Mappers;

namespace ACleanAPI.Example.Infrastructure.Tests;

[TestClass]
public sealed class UserModelMapperTests
{
    private UserModelMapper _mapper;

    public UserModelMapperTests()
    {
        _mapper = new UserModelMapper();
    }

    [TestMethod]
    public void MapToEntity_ThrowsArgumentNullException_WhenModelIsNull()
    {
        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => _mapper.MapToEntity(null));
    }

    [TestMethod]
    public void MapToEntity_MapsPropertiesCorrectly()
    {
        // Arrange
        var model = new UserModel
        {
            Id = 5,
            FirstName = "Jean",
            LastName = "Martin",
            Email = "jean.martin@email.com"
        };

        // Act
        var entity = _mapper.MapToEntity(model);

        // Assert
        Assert.IsNotNull(entity);
        Assert.AreEqual(model.Id, entity.Id);
        Assert.AreEqual(model.FirstName, entity.FirstName);
        Assert.AreEqual(model.LastName, entity.LastName);
        Assert.AreEqual(model.Email, entity.Email);
    }
}
