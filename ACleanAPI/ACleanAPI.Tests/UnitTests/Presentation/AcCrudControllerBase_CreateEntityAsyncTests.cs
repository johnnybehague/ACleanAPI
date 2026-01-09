using ACleanAPI.Application.Requests;
using ACleanAPI.Tests.App.Presentation;
using ACleanAPI.Tests.Common;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Presentation;

[TestClass]
public class AcCrudControllerBase_CreateEntityAsyncTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserTestController _controller;

    public AcCrudControllerBase_CreateEntityAsyncTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UserTestController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task CreateEntityAsync_ModelStateInvalid_ReturnsBadRequestWithModelState()
    {
        // Arrange
        _controller.ModelState.AddModelError("Dto", "Dto is required");
        var request = new AcCreateEntityRequest<UserTestDto>(null);

        // Act
        var result = await _controller.CreateEntityAsync(request);

        // Assert
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.IsFalse(_controller.ModelState.IsValid);
        _mediatorMock.Verify(m => m.Send(It.IsAny<AcCreateEntityRequest<UserTestDto>>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task CreateEntityAsync_MediatorReturnsFailedResult_ReturnsBadRequest()
    {
        // Arrange
        var request = new AcCreateEntityRequest<UserTestDto>(new UserTestDto());
        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Fail"));

        // Act
        var result = await _controller.CreateEntityAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(result);
        _mediatorMock.Verify(m => m.Send(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task CreateEntityAsync_MediatorReturnsSuccess_ReturnsNoContent()
    {
        // Arrange
        var request = new AcCreateEntityRequest<UserTestDto>(new UserTestDto());
        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok());

        // Act
        var result = await _controller.CreateEntityAsync(request);

        // Assert
        Assert.IsInstanceOfType<NoContentResult>(result);
        _mediatorMock.Verify(m => m.Send(request, It.IsAny<CancellationToken>()), Times.Once);
    }
}
