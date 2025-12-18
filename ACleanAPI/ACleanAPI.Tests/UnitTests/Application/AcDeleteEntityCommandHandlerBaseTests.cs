using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.App.Application;
using ACleanAPI.Tests.Common;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Application;

[TestClass]
public class AcDeleteEntityCommandHandlerBaseTests
{
    private readonly Mock<IAcEntityRepository<UserTestEntity>> _repositoryMock;
    private readonly DeleteUserTestCommandHandler _handler;

    public AcDeleteEntityCommandHandlerBaseTests()
    {
        _repositoryMock = new Mock<IAcEntityRepository<UserTestEntity>>();
        _handler = new DeleteUserTestCommandHandler(_repositoryMock.Object);
    }

    [TestMethod]
    public async Task HandleRequest_IdIsNull_ReturnsFailResult()
    {
        // Arrange
        var requestMock = new Mock<IAcDeleteEntityRequest>();
        requestMock.Setup(r => r.Id).Returns((int?)null);

        // Act
        var result = await _handler.HandleRequest(requestMock.Object, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Id is required.", result.Errors[0].Message);
        _repositoryMock.Verify(r => r.DeleteEntityAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task HandleRequest_ValidId_DeletesEntityAndReturnsOk()
    {
        // Arrange
        var entityId = 1;

        var requestMock = new Mock<IAcDeleteEntityRequest>();
        requestMock.Setup(r => r.Id).Returns(entityId);

        _repositoryMock
            .Setup(r => r.DeleteEntityAsync(entityId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.HandleRequest(requestMock.Object, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(0, result.Errors.Count);
        _repositoryMock.Verify(r => r.DeleteEntityAsync(entityId, It.IsAny<CancellationToken>()),Times.Once);
    }

    [TestMethod]
    public async Task HandleRequest_RepositoryThrowsException_ReturnsFailResult()
    {
        // Arrange
        var entityId = 1;
        var exceptionMessage = "DB error";

        var requestMock = new Mock<IAcDeleteEntityRequest>();
        requestMock.Setup(r => r.Id).Returns(entityId);

        _repositoryMock
            .Setup(r => r.DeleteEntityAsync(entityId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception(exceptionMessage));

        // Act
        var result = await _handler.HandleRequest(requestMock.Object, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual($"Error deleting entity: {exceptionMessage}", result.Errors[0].Message);

        _repositoryMock.Verify(r => r.DeleteEntityAsync(entityId, It.IsAny<CancellationToken>()), Times.Once);
    }
}
