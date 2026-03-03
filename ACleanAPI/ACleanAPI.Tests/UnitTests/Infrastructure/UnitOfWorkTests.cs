using ACleanAPI.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Infrastructure;

[TestClass]
public class UnitOfWorkTests
{
    private Mock<DbContext> _dbContextMock;
    private Mock<DatabaseFacade> _databaseMock;
    private Mock<IDbContextTransaction> _transactionMock;
    private UnitOfWork _unitOfWork;

    public UnitOfWorkTests()
    {
        _dbContextMock = new Mock<DbContext>(new DbContextOptions<DbContext>());
        _databaseMock = new Mock<DatabaseFacade>(_dbContextMock.Object);
        _transactionMock = new Mock<IDbContextTransaction>();
        _unitOfWork = new UnitOfWork(_dbContextMock.Object);
    }

    [TestInitialize]
    public void Setup()
    {
        _dbContextMock.SetupGet(c => c.Database).Returns(_databaseMock.Object);
    }

    [TestMethod]
    public async Task BeginTransactionAsync_StartsTransaction_WhenNoneActive()
    {
        // Arrange
        _databaseMock.Setup(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);

        // Act
        await _unitOfWork.BeginTransactionAsync();

        // Assert
        _databaseMock.Verify(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task BeginTransactionAsync_DoesNotStartTransaction_WhenAlreadyActive()
    {
        // Arrange
        _databaseMock.Setup(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);

        await _unitOfWork.BeginTransactionAsync();
        await _unitOfWork.BeginTransactionAsync();

        // Assert
        _databaseMock.Verify(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task CommitAsync_CommitsAndDisposesTransaction()
    {
        // Arrange
        _databaseMock.Setup(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);
        _dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        _transactionMock.Setup(t => t.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _transactionMock.Setup(t => t.DisposeAsync())
            .Returns(ValueTask.CompletedTask);

        await _unitOfWork.BeginTransactionAsync();

        // Act
        await _unitOfWork.CommitAsync();

        // Assert
        _dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transactionMock.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transactionMock.Verify(t => t.DisposeAsync(), Times.Once);
    }

    [TestMethod]
    public async Task CommitAsync_RollsBackAndThrows_WhenSaveChangesFails()
    {
        // Arrange
        _databaseMock.Setup(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);
        _dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Save failed"));
        _transactionMock.Setup(t => t.RollbackAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _transactionMock.Setup(t => t.DisposeAsync())
            .Returns(ValueTask.CompletedTask);

        await _unitOfWork.BeginTransactionAsync();

        // Act & Assert
        await Assert.ThrowsExceptionAsync<Exception>(async () => await _unitOfWork.CommitAsync());
        _transactionMock.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transactionMock.Verify(t => t.DisposeAsync(), Times.Once);
    }

    [TestMethod]
    public async Task CommitAsync_Throws_WhenNoActiveTransaction()
    {
        // Act & Assert
        await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () => await _unitOfWork.CommitAsync());
    }

    [TestMethod]
    public async Task RollbackAsync_RollsBackAndDisposes_WhenActive()
    {
        // Arrange
        _databaseMock.Setup(d => d.BeginTransactionAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(_transactionMock.Object);
        _transactionMock.Setup(t => t.RollbackAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _transactionMock.Setup(t => t.DisposeAsync())
            .Returns(ValueTask.CompletedTask);

        await _unitOfWork.BeginTransactionAsync();

        // Act
        await _unitOfWork.RollbackAsync();

        // Assert
        _transactionMock.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Once);
        _transactionMock.Verify(t => t.DisposeAsync(), Times.Once);
    }

    [TestMethod]
    public async Task RollbackAsync_DoesNothing_WhenNoActiveTransaction()
    {
        // Act
        await _unitOfWork.RollbackAsync();

        // Assert
        // No exception, no calls to transaction
        _transactionMock.Verify(t => t.RollbackAsync(It.IsAny<CancellationToken>()), Times.Never);
        _transactionMock.Verify(t => t.DisposeAsync(), Times.Never);
    }

    [TestMethod]
    public async Task SaveChangesAsync_CallsDbContextSaveChangesAsync()
    {
        // Arrange
        _dbContextMock.Setup(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _unitOfWork.SaveChangesAsync();

        // Assert
        _dbContextMock.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}
