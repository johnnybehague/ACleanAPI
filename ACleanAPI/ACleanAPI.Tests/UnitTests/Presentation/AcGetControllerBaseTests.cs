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
public sealed class AcGetControllerBaseTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserTestController _controller;

    public AcGetControllerBaseTests()
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
            .ReturnsAsync(Result.Ok(detail));

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
        var request = Mock.Of<IRequest<Result<UserTestDetailDto>>>();

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok<UserTestDetailDto?>(null));

        // Act
        var action = await _controller.GetEntityByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(action.Result);
    }

    [TestMethod]
    public async Task GetEntityAsync_ReturnsBadRequest_WhenQueryFails()
    {
        // Arrange
        var request = Mock.Of<IRequest<Result<UserTestDetailDto>>>();

        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<UserTestDetailDto>("error"));

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
        var request = Mock.Of<IRequest<Result<UserTestDetailDto>>>();

        // Act
        var action = await _controller.GetEntityByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(action.Result);
    }
}
