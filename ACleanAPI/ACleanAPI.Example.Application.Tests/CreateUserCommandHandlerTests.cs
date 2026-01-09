using ACleanAPI.Example.Application.Users.Commands;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using Moq;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public class CreateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserMapper> _userMapper;
    private readonly CreateUserCommandHandler _handler;

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userMapper = new Mock<IUserMapper>();
        _handler = new CreateUserCommandHandler(_userRepositoryMock.Object, _userMapper.Object);
    }

    [TestMethod]
    public async Task Handle_WithValidCommand_DelegatesToBaseHandler()
    {
        // Arrange
        var dto = new UserDto { Id = 1 };
        var entity = new User { Id = 1 };
        var command = new CreateUserCommand(dto);

        _userMapper.Setup(m => m.MapToEntity(dto))
            .Returns(entity);

        _userRepositoryMock
            .Setup(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        _userRepositoryMock.Verify(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ImplementsIRequestHandlerContract()
    {
        // Arrange
        var dto = new UserDto { Id = 1 };
        var entity = new User { Id = 1 };
        var command = new CreateUserCommand(dto);

        _userMapper.Setup(m => m.MapToEntity(dto))
            .Returns(entity);

        _userRepositoryMock
            .Setup(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }
}
