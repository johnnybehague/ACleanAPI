using ACleanAPI.Application.Commands;
using ACleanAPI.Application.Queries;
using ACleanAPI.Presentation.Interfaces;
using ACleanAPI.Tests.App;
using ACleanAPI.Tests.App.Presentation;
using ACleanAPI.Tests.Common;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Presentation;

[TestClass]
public sealed class AcCrudControllerBaseTests
{
    private readonly Mock<IAcMediator> _mediatorMock;
    private readonly UserTestController _controller;

    public AcCrudControllerBaseTests()
    {
        _mediatorMock = new Mock<IAcMediator>();
        _controller = new UserTestController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("field", "Error message");
        var request = new AcGetEntitiesQuery<UserTestDto>();

        // Act
        var action = await _controller.GetAllAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(action.Result);
    }


    [TestMethod]
    public async Task GetAllAsync_ReturnsOk_WhenQuerySuccess()
    {
        // Arrange
        var request = new AcGetEntitiesQuery<UserTestDto>();
        var expectedList = new List<UserTestDto> { new UserTestDto { Id = 1 } };

        _mediatorMock
            .Setup(m => m.QueryAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok<IEnumerable<UserTestDto>>(expectedList));

        // Act
        var action = await _controller.GetAllAsync(request);

        // Assert
        var ok = action.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(200, ok.StatusCode);
        Assert.AreEqual(expectedList, ok.Value);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsBadRequest_WhenQueryFails()
    {
        // Arrange
        var request = new AcGetEntitiesQuery<UserTestDto>();

        _mediatorMock
            .Setup(m => m.QueryAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail<IEnumerable<UserTestDto>>("some error"));

        // Act
        var action = await _controller.GetAllAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(action.Result);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsOk_WhenValueExists()
    {
        // Arrange
        int userId = 10;
        var request = new AcGetEntityByIdQuery<UserTestDetailDto>(userId);
        var detail = new UserTestDetailDto { Id = userId };
        _mediatorMock
            .Setup(m => m.QueryAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok<UserTestDetailDto>(detail));

        // Act
        var action = await _controller.GetByIdAsync(request);

        // Assert
        var ok = action.Result as OkObjectResult;
        Assert.IsNotNull(ok);
        Assert.AreEqual(200, ok.StatusCode);
        Assert.AreEqual(detail, ok.Value);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsNotFound_WhenValueIsNull()
    {
        // Arrange
        var request = new AcGetEntityByIdQuery<UserTestDetailDto>(null);

        _mediatorMock
            .Setup(m => m.QueryAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("ENTITY_NOT_FOUND"));

        // Act
        var action = await _controller.GetByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<NotFoundResult>(action.Result);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsBadRequest_WhenQueryFails()
    {
        // Arrange
        var request = new AcGetEntityByIdQuery<UserTestDetailDto>(null);

        _mediatorMock
            .Setup(m => m.QueryAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Some error"));

        // Act
        var action = await _controller.GetByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(action.Result);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsBadRequest_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("field", "Error message");
        var request = new AcGetEntityByIdQuery<UserTestDetailDto>(null);

        // Act
        var action = await _controller.GetByIdAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestObjectResult>(action.Result);
    }

    [TestMethod]
    public async Task DeleteAsync_ModelStateInvalid_ReturnsBadRequestWithModelState()
    {
        // Arrange
        _controller.ModelState.AddModelError("Id", "Id is required");
        var request = new AcDeleteEntityCommand(null);

        // Act
        var result = await _controller.DeleteAsync(request);

        // Assert
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.IsFalse(_controller.ModelState.IsValid);
        _mediatorMock.Verify(m => m.SendAsync(It.IsAny<AcDeleteEntityCommand>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task DeleteAsync_MediatorReturnsFailedResult_ReturnsBadRequest()
    {
        // Arrange
        var request = new AcDeleteEntityCommand(null);
        _mediatorMock
            .Setup(m => m.SendAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Fail"));

        // Act
        var result = await _controller.DeleteAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(result);
        _mediatorMock.Verify(m => m.SendAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task DeleteAsync_MediatorReturnsSuccess_ReturnsNoContent()
    {
        // Arrange
        var request = new AcDeleteEntityCommand(null);
        _mediatorMock
            .Setup(m => m.SendAsync(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok());

        // Act
        var result = await _controller.DeleteAsync(request);

        // Assert
        Assert.IsInstanceOfType<NoContentResult>(result);
        _mediatorMock.Verify(m => m.SendAsync(request, It.IsAny<CancellationToken>()), Times.Once);
    }
}
