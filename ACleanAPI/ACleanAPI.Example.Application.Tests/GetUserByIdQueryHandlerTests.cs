using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Application.Users.Queries.GetUserById;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using Moq;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public sealed class GetUserByIdQueryHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserDetailMapper> _userDetailMapperMock;
    private readonly GetUserByIdQueryHandler _handler;

    public GetUserByIdQueryHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userDetailMapperMock = new Mock<IUserDetailMapper>();
        _handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object, _userDetailMapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ReturnsUserDetailDto_WhenUserExists()
    {
        // Arrange
        var user = new User { Id = 1, Email = "test@mail.com", FirstName = "John", LastName = "Doe" };
        var dto = new UserDetailDto { Id = 1, Email = "test@mail.com", FirstName = "John", LastName = "Doe" };
        var query = new GetUserByIdQuery { Id = 1 };

        _userRepositoryMock
            .Setup(r => r.GetEntityByIdAsync(1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        _userDetailMapperMock
            .Setup(m => m.MapToDto(user))
            .Returns(dto);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(dto.Id, result.Value.Id);
        Assert.AreEqual(dto.Email, result.Value.Email);
        Assert.AreEqual(dto.FirstName, result.Value.FirstName);
        Assert.AreEqual(dto.LastName, result.Value.LastName);
    }

    [TestMethod]
    public async Task Handle_ReturnsSuccessWithNull_WhenUserDoesNotExist()
    {
        // Arrange
        var query = new GetUserByIdQuery { Id = 99 };
        _userRepositoryMock
            .Setup(r => r.GetEntityByIdAsync(99, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
    }
}
