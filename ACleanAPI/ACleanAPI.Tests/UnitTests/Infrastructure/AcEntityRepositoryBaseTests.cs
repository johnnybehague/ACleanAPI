using ACleanAPI.Infrastructure.Interfaces;
using ACleanAPI.Tests.App;
using ACleanAPI.Tests.App.Infrastructure;
using ACleanAPI.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Infrastructure;

[TestClass]
public sealed class AcEntityRepositoryBaseTests
{
    private AppTestDbContext _context;
    private Mock<IAcModelMapper<UserTestModel, UserTestEntity>> _mapperMock;
    private UserTestRepository _repository;

    public AcEntityRepositoryBaseTests()
    {
        _context = new AppTestDbContext(new DbContextOptions<AppTestDbContext>());
        _mapperMock = new Mock<IAcModelMapper<UserTestModel, UserTestEntity>>();
        _repository = new UserTestRepository(_context, _mapperMock.Object);
    }

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppTestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDb_" + Guid.NewGuid())
            .Options;

        _context = new AppTestDbContext(options);
        _repository = new UserTestRepository(_context, _mapperMock.Object);
    }

    [TestMethod]
    public async Task GetEntitiesAsync_ReturnsMappedEntities()
    {
        // Arrange
        _context.Users.AddRange(
            new UserTestModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com" },
            new UserTestModel { Id = 2, FirstName = "Jane", LastName = "Dean", Email = "jane@dean.com" }
        );
        await _context.SaveChangesAsync();

        _mapperMock.Setup(m => m.MapToEntity(It.IsAny<UserTestModel>()))
            .Returns<UserTestModel>(m => new UserTestEntity { Id = m.Id, FirstName = m.FirstName, LastName = m.LastName, Email = m.Email });

        // Act
        var result = await _repository.GetEntitiesAsync(CancellationToken.None);

        // Assert
        Assert.AreEqual(2, result.Count());

        _mapperMock.Verify(m => m.MapToEntity(It.Is<UserTestModel>(x => x.Id == 1)), Times.Once);
        _mapperMock.Verify(m => m.MapToEntity(It.Is<UserTestModel>(x => x.Id == 2)), Times.Once);
    }

    [TestMethod]
    public async Task GetEntityByIdAsync_ReturnsMappedEntity()
    {
        // Arrange
        var entity = new UserTestModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com" };
        _context.Users.Add(entity);
        await _context.SaveChangesAsync();

        _mapperMock.Setup(m => m.MapToEntity(entity))
            .Returns(new UserTestEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com" });

        // Act
        var result = await _repository.GetEntityByIdAsync(1, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);

        _mapperMock.Verify(m => m.MapToEntity(It.Is<UserTestModel>(x => x.Id == 1)), Times.Once);
    }

    [TestMethod]
    public async Task CreateEntityAsync_ReturnTaskValid()
    {
        // Arrange
        var entity = new UserTestEntity { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com" };

        var model = new UserTestModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@doe.com" };
        await _context.SaveChangesAsync();

        _mapperMock.Setup(m => m.MapToModel(entity))
            .Returns(model);

        // Act
        await _repository.CreateEntityAsync(entity, CancellationToken.None);

        // Assert
        _mapperMock.Verify(m => m.MapToModel(It.Is<UserTestEntity>(x => x.Id == 1)), Times.Once);
    }
}
