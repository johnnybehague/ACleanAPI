using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Application.Users.Queries;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using Moq;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public sealed class GetUsersQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserMapper> _userMapperMock;
    private readonly GetUsersQueryHandler _handler;

    public GetUsersQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userMapperMock = new Mock<IUserMapper>();
        _handler = new GetUsersQueryHandler(_userRepositoryMock.Object, _userMapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsMappedUserDtos_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = 1, FirstName = "Alice", LastName = "Dupont" },
                new User { Id = 2, FirstName = "Bob", LastName = "Martin" }
            };
        var dtos = new List<UserDto>
            {
                new UserDto { Id = 1, FirstName = "Alice", LastName = "Dupont" },
                new UserDto { Id = 2, FirstName = "Bob", LastName = "Martin" }
            };

        _userRepositoryMock
            .Setup(r => r.GetEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        _userMapperMock
            .Setup(m => m.MapToDto(users[0]))
            .Returns(dtos[0]);
        _userMapperMock
            .Setup(m => m.MapToDto(users[1]))
            .Returns(dtos[1]);

        var query = new GetUsersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        var resultList = result.Value.ToList();
        Assert.AreEqual(2, resultList.Count);
        Assert.AreEqual(dtos[0].Id, resultList[0].Id);
        Assert.AreEqual(dtos[1].Id, resultList[1].Id);
    }

    [TestMethod]
    public async Task Handle_ReturnsEmptyList_WhenNoUsersExist()
    {
        // Arrange
        _userRepositoryMock
            .Setup(r => r.GetEntitiesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<User>());

        var query = new GetUsersQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.Value.Count());
    }
}
