using ACleanAPI.Application.Interfaces;
using ACleanAPI.Tests.App;
using ACleanAPI.Tests.App.Presentation;
using ACleanAPI.Tests.Common;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Presentation;

[TestClass]
public sealed class AcCrudControllerBaseTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserTestController _controller;

    public AcCrudControllerBaseTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UserTestController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task GetEntitiesAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("field", "Error message");
        var request = Mock.Of<IAcGetEntitiesRequest<UserTestDto>>();

        // Act
        var action = await _controller.GetEntitiesAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(action.Result);
    }


    [TestMethod]
    public async Task GetEntitiesAsync_ReturnsOk_WhenQuerySuccess()
    {
        // Arrange
        var request = Mock.Of<IAcGetEntitiesRequest<UserTestDto>>();
        var expectedList = new List<UserTestDto> { new UserTestDto { Id = 1 } };

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok<IEnumerable<UserTestDto>>(expectedList));

        // Act
        var action = await _controller.GetEntitiesAsync(request);

        // Assert
        var ok = action.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(200, ok.StatusCode);
        Assert.AreEqual(expectedList, ok.Value);
    }

    [TestMethod]
    public async Task GetEntitiesAsync_ReturnsBadRequest_WhenQueryFails()
    {
        // Arrange
        var request = Mock.Of<IAcGetEntitiesRequest<UserTestDto>>();

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<IEnumerable<UserTestDto>>("some error"));

        // Act
        var action = await _controller.GetEntitiesAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(action.Result);
    }

    [TestMethod]
    public async Task GetEntityAsync_ReturnsOk_WhenValueExists()
    {
        // Arrange
        var request = Mock.Of<IAcGetEntityByIdRequest<UserTestDetailDto>>();
        var detail = new UserTestDetailDto { Id = 10 };
        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok<UserTestDetailDto>(detail));

        // Act
        var action = await _controller.GetEntityByIdAsync(request);

        // Assert
        var ok = action.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(200, ok.StatusCode);
        Assert.AreEqual(detail, ok.Value);
    }

    [TestMethod]
    public async Task GetEntityAsync_ReturnsNotFound_WhenValueIsNull()
    {
        // Arrange
        var request = Mock.Of<IAcGetEntityByIdRequest<UserTestDetailDto>>();

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("ENTITY_NOT_FOUND"));

        // Act
        var action = await _controller.GetEntityByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(action.Result);
    }

    [TestMethod]
    public async Task GetEntityAsync_ReturnsBadRequest_WhenQueryFails()
    {
        // Arrange
        var request = Mock.Of<IAcGetEntityByIdRequest<UserTestDetailDto>>();

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Some error"));

        // Act
        var action = await _controller.GetEntityByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(action.Result);
    }

    [TestMethod]
    public async Task GetEntityAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("field", "Error message");
        var request = Mock.Of<IAcGetEntityByIdRequest<UserTestDetailDto>>();

        // Act
        var action = await _controller.GetEntityByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(action.Result);
    }

    [TestMethod]
    public async Task DeleteEntityAsync_ModelStateInvalid_ReturnsBadRequestWithModelState()
    {
        // Arrange
        _controller.ModelState.AddModelError("Id", "Id is required");
        var requestMock = new Mock<IAcDeleteEntityRequest>();

        // Act
        var result = await _controller.DeleteEntityAsync(requestMock.Object);

        // Assert
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.IsFalse(_controller.ModelState.IsValid);
        _mediatorMock.Verify(m => m.Send(It.IsAny<IAcDeleteEntityRequest>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task DeleteEntityAsync_MediatorReturnsFailedResult_ReturnsBadRequest()
    {
        // Arrange
        var requestMock = new Mock<IAcDeleteEntityRequest>();
        _mediatorMock
            .Setup(m => m.Send(requestMock.Object, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Fail"));

        // Act
        var result = await _controller.DeleteEntityAsync(requestMock.Object);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        _mediatorMock.Verify(m => m.Send(requestMock.Object, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task DeleteEntityAsync_MediatorReturnsSuccess_ReturnsNoContent()
    {
        // Arrange
        var requestMock = new Mock<IAcDeleteEntityRequest>();
        _mediatorMock
            .Setup(m => m.Send(requestMock.Object, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok());

        // Act
        var result = await _controller.DeleteEntityAsync(requestMock.Object);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NoContentResult));
        _mediatorMock.Verify(m => m.Send(requestMock.Object, It.IsAny<CancellationToken>()), Times.Once);
    }
}
