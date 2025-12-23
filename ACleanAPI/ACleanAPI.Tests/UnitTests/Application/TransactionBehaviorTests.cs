using ACleanAPI.Application.Behaviors;
using ACleanAPI.Application.Interfaces;
using MediatR;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Application;

[TestClass]
public class TransactionBehaviorTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;

    public TransactionBehaviorTests()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [TestMethod]
    public async Task Handle_CommitsTransaction_WhenNoException()
    {
        // Arrange
        var behavior = new TransactionBehavior<string, string>(_unitOfWorkMock.Object);
        var request = "test";
        var expectedResponse = "response";
        RequestHandlerDelegate<string> next = (r) => Task.FromResult(expectedResponse);

        // Act
        var result = await behavior.Handle(request, next, CancellationToken.None);

        // Assert
        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.RollbackAsync(It.IsAny<CancellationToken>()), Times.Never);
        Assert.AreEqual(expectedResponse, result);
    }

    [TestMethod]
    public async Task Handle_RollsBackTransaction_WhenExceptionThrown()
    {
        // Arrange
        var behavior = new TransactionBehavior<string, string>(_unitOfWorkMock.Object);
        var request = "test";
        RequestHandlerDelegate<string> next = (r) => Task.FromException<string>(new InvalidOperationException("error"));

        // Act & Assert
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
        {
            await behavior.Handle(request, next, CancellationToken.None);
        });

        _unitOfWorkMock.Verify(u => u.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
