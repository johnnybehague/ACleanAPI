using ACleanAPI.Application.Interfaces;
using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.App.Application;
using ACleanAPI.Tests.Common;
using Moq;
namespace ACleanAPI.Tests.UnitTests.Application;

[TestClass]
public sealed class AcGetEntityByIdQueryHandlerBaseTests
{
    private readonly Mock<IAcEntityRepository<UserTestEntity>> _repositoryMock;
    private readonly Mock<IAcEntityMapper<UserTestEntity, UserTestDto>> _mapperMock;
    private readonly GetUserByIdTestQueryHandler _handler;

    public AcGetEntityByIdQueryHandlerBaseTests()
    {
        _repositoryMock = new Mock<IAcEntityRepository<UserTestEntity>>();
        _mapperMock = new Mock<IAcEntityMapper<UserTestEntity, UserTestDto>>();
        _handler = new GetUserByIdTestQueryHandler(
            _repositoryMock.Object,
            _mapperMock.Object);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnFailWhenIdIsNull()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntityByIdRequest<UserTestDto>>();
        requestMock.Setup(r => r.Id).Returns((int?)null);
        var cancellationToken = CancellationToken.None;

        // Act
        var result = await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        Assert.IsTrue(result.IsFailed);
        Assert.AreEqual("Id is required.", result.Errors[0].Message);
    }

    [TestMethod]
    public async Task Handle_ShouldCallRepository()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntityByIdRequest<UserTestDto>>();
        requestMock.Setup(r => r.Id).Returns(1);
        var cancellationToken = CancellationToken.None;

        // Act
        await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        _repositoryMock.Verify(r => r.GetEntityByIdAsync(It.IsAny<int>(), cancellationToken), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldMapEntityToDto()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntityByIdRequest<UserTestDto>>();
        requestMock.Setup(r => r.Id).Returns(1);
        var cancellationToken = CancellationToken.None;
        var mockedTestEntity = new UserTestEntity { Id = 1 };

        _repositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>(), cancellationToken))
            .ReturnsAsync(mockedTestEntity);
        _mapperMock.Setup(m => m.MapToDto(It.IsAny<UserTestEntity>()))
            .Returns(new UserTestDto { Id = 1 }); // Revoir le code
        // Act
        await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        _mapperMock.Verify(r => r.MapToDto(It.IsAny<UserTestEntity>()), Times.Once);
    }

    [TestMethod]
    public async Task Handle_ShouldReturnsOkWithDto()
    {
        // Arrange
        var requestMock = new Mock<IAcGetEntityByIdRequest<UserTestDto>>();
        requestMock.Setup(r => r.Id).Returns(1);
        var cancellationToken = CancellationToken.None;
        var mockedTestEntity = new UserTestEntity { Id = 1, FirstName = "John", LastName = "Doe" };

        _repositoryMock.Setup(r => r.GetEntityByIdAsync(It.IsAny<int>(), cancellationToken))
            .ReturnsAsync(mockedTestEntity);
        _mapperMock.Setup(m => m.MapToDto(It.IsAny<UserTestEntity>()))
            .Returns(new UserTestDto { Id = 1, FirstName = "John", LastName = "Doe" });

        // Act
        var result = await _handler.Handle(requestMock.Object, cancellationToken);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(mockedTestEntity.Id, result.Value.Id);
        Assert.AreEqual(mockedTestEntity.FirstName, result.Value.FirstName);
        Assert.AreEqual(mockedTestEntity.LastName, result.Value.LastName);
    }
}
