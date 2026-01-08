using ACleanAPI.Example.Application.Users.Commands;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Mappers;
using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Domain.Users.Interfaces;
using Moq;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public class UpdateUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IUserMapper> _userMapper;
    private readonly UpdateUserCommandHandler _handler;

    public UpdateUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userMapper = new Mock<IUserMapper>();
        _handler = new UpdateUserCommandHandler(_userRepositoryMock.Object, _userMapper.Object);
    }

    [TestMethod]
    public async Task Handle_WithValidCommand_DelegatesToBaseHandler()
    {
        // Arrange
        var id = 1;
        var dto = new UserDto { Id = 1 };
        var entity = new User { Id = 1 };
        var command = new UpdateUserCommand { Id = id, Dto = dto };

        _userMapper.Setup(m => m.MapToEntity(dto))
            .Returns(entity);

        _userRepositoryMock
            .Setup(r => r.UpdateEntityAsync(1, entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        _userRepositoryMock.Verify(r => r.UpdateEntityAsync(id, entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ImplementsIRequestHandlerContract()
    {
        // Arrange
        var id = 1;
        var dto = new UserDto { Id = 1 };
        var entity = new User { Id = 1 };
        var command = new UpdateUserCommand { Id = id, Dto = dto };

        _userMapper.Setup(m => m.MapToEntity(dto))
            .Returns(entity);

        _userRepositoryMock
            .Setup(r => r.UpdateEntityAsync(id, entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }
}
