using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.App.Application;
using ACleanAPI.Tests.Common;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Application;

[TestClass]
public sealed class AcGetEntitiesQueryHandlerBaseTests
{
    private Mock<IAcGetEntitiesRepository<UserTestEntity>> _repositoryMock;
    private Mock<IAcEntityMapper<UserTestEntity, UserTestDto>> _mapperMock;
    private GetUsersTestQueryHandler _handler;

    [TestInitialize]
    public void Setup()
    {
        _repositoryMock = new Mock<IAcGetEntitiesRepository<UserTestEntity>>();
        _mapperMock = new Mock<IAcEntityMapper<UserTestEntity, UserTestDto>>();
        _handler = new GetUsersTestQueryHandler(
            _repositoryMock.Object,
            _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ShouldCallRepository()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntitiesRequest<UserTestDto>>();
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        _repositoryMock.Verify(r => r.GetEntitiesAsync(cancellationToken), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldMapEntitiesToDtos()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntitiesRequest<UserTestDto>>();
        var cancellationToken = CancellationToken.None;
        var entities = new List<UserTestEntity>
        {
            new UserTestEntity { Id = 1 },
            new UserTestEntity { Id = 2 }
        };
        _repositoryMock.Setup(r => r.GetEntitiesAsync(cancellationToken))
            .ReturnsAsync(entities);

        // Act
        var result = await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        _mapperMock.Verify(r => r.MapToDto(It.IsAny<UserTestEntity>()), Times.Exactly(entities.Count));
    }

    [TestMethod]
    public async Task Handle_ShouldReturnsOkWithDtos()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntitiesRequest<UserTestDto>>();
        var cancellationToken = CancellationToken.None;
        var entities = new List<UserTestEntity>
        {
            new UserTestEntity { Id = 1 },
            new UserTestEntity { Id = 2 }
        };
        _repositoryMock.Setup(r => r.GetEntitiesAsync(cancellationToken))
            .ReturnsAsync(entities);

        // Act
        var result = await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(entities.Count, result.Value.Count());
    }
}
