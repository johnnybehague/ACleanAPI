using ACleanAPI.Application.Interfaces;
using ACleanAPI.Application.Requests;
using ACleanAPI.Tests.App.Presentation;
using ACleanAPI.Tests.Common;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Presentation;

[TestClass]
public class AcCrudControllerBase_UpdateEntityAsyncTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UserTestController _controller;

    public AcCrudControllerBase_UpdateEntityAsyncTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new UserTestController(_mediatorMock.Object);
    }

    [TestMethod]
    public async Task UpdateEntityAsync_ModelStateInvalid_ReturnsBadRequestWithModelState()
    {
        // Arrange
        _controller.ModelState.AddModelError("Dto", "Dto is required");
        var request = new AcUpdateEntityRequest<UserTestDto>(1, null);

        // Act
        var result = await _controller.UpdateEntityAsync(request);

        // Assert
        var badRequest = result as BadRequestObjectResult;
        Assert.IsNotNull(badRequest);
        Assert.IsFalse(_controller.ModelState.IsValid);
        _mediatorMock.Verify(m => m.Send(request, It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestMethod]
    public async Task UpdateEntityAsync_MediatorReturnsFailedResult_ReturnsBadRequest()
    {
        // Arrange
        var request = new AcUpdateEntityRequest<UserTestDto>(0, null);
        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Fail("Fail"));

        // Act
        var result = await _controller.UpdateEntityAsync(request);

        // Assert
        Assert.IsInstanceOfType<BadRequestResult>(result);
        _mediatorMock.Verify(m => m.Send(request, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task UpdateEntityAsync_MediatorReturnsSuccess_ReturnsNoContent()
    {
        // Arrange
        var request = new AcUpdateEntityRequest<UserTestDto>(1, new UserTestDto { Id = 1 });
        _mediatorMock
            .Setup(m => m.Send(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Ok());

        // Act
        var result = await _controller.UpdateEntityAsync(request);

        // Assert
        Assert.IsInstanceOfType<NoContentResult>(result);
        _mediatorMock.Verify(m => m.Send(request, It.IsAny<CancellationToken>()), Times.Once);
    }
}
