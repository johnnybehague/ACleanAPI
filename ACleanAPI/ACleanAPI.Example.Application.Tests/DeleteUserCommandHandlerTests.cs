using ACleanAPI.Example.Application.Users.Commands;
using ACleanAPI.Example.Domain.Users.Interfaces;
using FluentResults;
using MediatR;
using Moq;

namespace ACleanAPI.Example.Application.Tests;

[TestClass]
public class DeleteUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _handler = new DeleteUserCommandHandler(_userRepositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_WithValidCommand_DelegatesToBaseHandler()
    {
        // Arrange
        var userId = 1;
        var command = new DeleteUserCommand(userId);

        _userRepositoryMock
            .Setup(r => r.DeleteEntityAsync(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);

        _userRepositoryMock.Verify(r => r.DeleteEntityAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ImplementsIRequestHandlerContract()
    {
        // Arrange
        var userId = 1;
        var command = new DeleteUserCommand(userId);

        _userRepositoryMock
            .Setup(r => r.DeleteEntityAsync(userId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
    }
}
