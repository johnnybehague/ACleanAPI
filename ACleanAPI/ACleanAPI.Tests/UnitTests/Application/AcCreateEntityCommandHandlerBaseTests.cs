using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Requests;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.App.Application;
using ACleanAPI.Tests.Common;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Application;

[TestClass]
public class AcCreateEntityCommandHandlerBaseTests
{
    private readonly Mock<IAcEntityRepository<UserTestEntity>> _repositoryMock;
    private readonly Mock<IAcEntityMapper<UserTestEntity, UserTestDto>> _mapperMock;
    private readonly CreateUserTestCommandHandler _handler;

    public AcCreateEntityCommandHandlerBaseTests()
    {
        _repositoryMock = new Mock<IAcEntityRepository<UserTestEntity>>();
        _mapperMock = new Mock<IAcEntityMapper<UserTestEntity, UserTestDto>>();
        _handler = new CreateUserTestCommandHandler(_repositoryMock.Object, _mapperMock.Object);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsFail_WhenDtoIsNull()
    {
        // Arrange
        var request = new AcCreateEntityRequest<UserTestDto>(null);

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        Assert.AreEqual("Entity is required.", result.Errors[0].Message);
        _mapperMock.Verify(m => m.MapToEntity(It.IsAny<UserTestDto>()), Times.Never);
        _repositoryMock.Verify(r => r.CreateEntityAsync(It.IsAny<UserTestEntity>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsOk_WhenEntityIsCreated()
    {
        // Arrange
        var dto = new UserTestDto();
        var entity = new UserTestEntity();
        var request = new AcCreateEntityRequest<UserTestDto>(dto);

        _mapperMock.Setup(m => m.MapToEntity(dto)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        _mapperMock.Verify(m => m.MapToEntity(dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task HandleRequest_ReturnsFail_WhenRepositoryThrowsException()
    {
        // Arrange
        var dto = new UserTestDto();
        var entity = new UserTestEntity();
        var request = new AcCreateEntityRequest<UserTestDto>(dto);

        _mapperMock.Setup(m => m.MapToEntity(dto)).Returns(entity);
        _repositoryMock.Setup(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("DB error"));

        // Act
        var result = await _handler.HandleRequest(request, CancellationToken.None);

        // Assert
        Assert.IsFalse(result.IsSuccess);
        StringAssert.Contains(result.Errors[0].Message, "Error creating entity: DB error");
        _mapperMock.Verify(m => m.MapToEntity(dto), Times.Once);
        _repositoryMock.Verify(r => r.CreateEntityAsync(entity, It.IsAny<CancellationToken>()), Times.Once);
    }
}
