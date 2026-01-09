using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.App.Application;
using ACleanAPI.Tests.Common;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Application;

[TestClass]
public class AcUpdateEntityCommandHandlerBaseTests
{
    private readonly Mock<IAcEntityRepository<UserTestEntity>> _repositoryMock;
    private readonly Mock<IAcEntityMapper<UserTestEntity, UserTestDto>> _mapperMock;
    private readonly UpdateUserTestCommandHandler _handler;

    public AcUpdateEntityCommandHandlerBaseTests()
    {
        _repositoryMock = new Mock<IAcEntityRepository<UserTestEntity>>();
        _mapperMock = new Mock<IAcEntityMapper<UserTestEntity, UserTestDto>>();
        _handler = new UpdateUserTestCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsFail_WhenIdIsNull()
    {
        // Arrange
        var id = 0;
        var dto = new UserTestDto { Id = 1 };
        var request = new AcUpdateEntityRequest<UserTestDto>(id, dto);

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Valid entity ID is required.", result.Errors[0].Message);
        _mapperMock.Verify(m => m.MapToEntity(It.IsAny<UserTestDto>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateEntityAsync(It.IsAny<int>(), It.IsAny<UserTestEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsFail_WhenDtoIsNull()
    {
        // Arrange
        var id = 1;
        var request = new AcUpdateEntityRequest<UserTestDto>(id, null);

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Entity is required.", result.Errors[0].Message);
        _mapperMock.Verify(m => m.MapToEntity(It.IsAny<UserTestDto>()), Times.Never);
        _repositoryMock.Verify(r => r.UpdateEntityAsync(It.IsAny<int>(), It.IsAny<UserTestEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsOk_WhenEntityIsUpdated()
    {
        // Arrange
        var id = 1;
        var dto = new UserTestDto { Id = 1 };
        var entity = new UserTestEntity { Id = 1 };
        var request = new AcUpdateEntityRequest<UserTestDto>(id, dto);

        _mapperMock.Setup(m => m.MapToEntity(dto)).Returns(entity);
        _repositoryMock.Setup(r => r.UpdateEntityAsync(id, entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        _mapperMock.Verify(m => m.MapToEntity(dto), Times.Once);
        _repositoryMock.Verify(r => r.UpdateEntityAsync(id, entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsFail_WhenRepositoryThrowsException()
    {
        // Arrange
        var id = 1;
        var dto = new UserTestDto { Id = 1 };
        var entity = new UserTestEntity { Id = 1 };
        var request = new AcUpdateEntityRequest<UserTestDto>(id, dto);

        _mapperMock.Setup(m => m.MapToEntity(dto)).Returns(entity);
        _repositoryMock.Setup(r => r.UpdateEntityAsync(id, entity, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        StringAssert.Contains(result.Errors[0].Message, "Error updating entity: DB error");
        _mapperMock.Verify(m => m.MapToEntity(dto), Times.Once);
        _repositoryMock.Verify(r => r.UpdateEntityAsync(id, entity, It.IsAny<CancellationToken>()), Times.Once);
    }
}
