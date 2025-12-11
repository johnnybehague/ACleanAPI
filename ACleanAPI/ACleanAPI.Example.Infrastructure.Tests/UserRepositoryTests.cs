using ACleanAPI.Example.Domain.Users.Entities;
using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Example.Infrastructure.Persistence;
using ACleanAPI.Example.Infrastructure.Users.Mappers;
using ACleanAPI.Example.Infrastructure.Users.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ACleanAPI.Example.Infrastructure.Tests;

[TestClass]
public sealed class UserRepositoryTests
{
    private AppDbContext _context;
    private Mock<IUserModelMapper> _mapperMock;
    private UserRepository _repository;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AppDbContext(options);

        _mapperMock = new Mock<IUserModelMapper>();
        _repository = new UserRepository(_context, _mapperMock.Object);

        // Arrange
        var userModels = new List<UserModel>
            {
                new UserModel { Id = 1, FirstName = "Alice", LastName = "Dupont", Email = "alice@dupont.com" },
                new UserModel { Id = 2, FirstName = "Bob", LastName = "Martin", Email = "bob@martin.com" }
            };
        _context.Users.AddRange(userModels);
        _context.SaveChanges();

        var users = new List<User>
            {
                new User { Id = 1, FirstName = "Alice", LastName = "Dupont", Email = "alice@dupont.com" },
                new User { Id = 2, FirstName = "Bob", LastName = "Martin", Email = "bob@martin.com" }
            };

        _mapperMock.Setup(m => m.MapToEntity(userModels[0])).Returns(users[0]);
        _mapperMock.Setup(m => m.MapToEntity(userModels[1])).Returns(users[1]);
    }

    [TestCleanup]
    public void Cleanup()
    {
        _context?.Dispose();
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsMappedUsers()
    {
        // Arrange

        // Act
        var result = await _repository.GetAllAsync(CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        var resultList = result.ToList();
        Assert.AreEqual(2, resultList.Count);
        Assert.AreEqual("Alice", resultList[0].FirstName);
        Assert.AreEqual("Bob", resultList[1].FirstName);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsMappedUser_WhenUserExists()
    {
        // Arrange

        // Act
        var result = await _repository.GetByIdAsync(1, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Alice", result.FirstName);
        Assert.AreEqual("Dupont", result.LastName);
        Assert.AreEqual("alice@dupont.com", result.Email);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsNull_WhenUserDoesNotExist()
    {
        // Act
        var result = await _repository.GetByIdAsync(999, CancellationToken.None);

        // Assert
        Assert.IsNull(result);
    }
}
