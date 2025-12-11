using ACleanAPI.Example.Infrastructure.Models;
using ACleanAPI.Example.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ACleanAPI.Example.Infrastructure.Tests;

//[TestClass]
//public sealed class AppDbContextTests
//{
//    private DbContextOptions<AppDbContext> _dbContextOptions;

//    [TestInitialize]
//    public void Setup()
//    {
//        _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
//            .UseInMemoryDatabase(databaseName: "TestDb")
//            .Options;
//    }

//    [TestMethod]
//    public void CanInsertUserIntoDatabase()
//    {
//        // Arrange
//        var user = new UserModel
//        {
//            Id = 1,
//            FirstName = "Alice",
//            LastName = "Dupont",
//            Email = "alice@dupont.com"
//        };

//        // Act
//        using (var context = new AppDbContext(_dbContextOptions))
//        {
//            context.Users.Add(user);
//            context.SaveChanges();
//        }

//        // Assert
//        using (var context = new AppDbContext(_dbContextOptions))
//        {
//            var users = context.Users.ToList();
//            Assert.AreEqual(1, users.Count);
//            Assert.AreEqual("Alice", users[0].FirstName);
//            Assert.AreEqual("Dupont", users[0].LastName);
//            Assert.AreEqual("alice@dupont.com", users[0].Email);
//        }
//    }

//    [TestMethod]
//    public void OnModelCreating_ConfiguresUserModelKey()
//    {
//        // Arrange & Act
//        using (var context = new AppDbContext(_dbContextOptions))
//        {
//            var entityType = context.Model.FindEntityType(typeof(UserModel));
//            var key = entityType.FindPrimaryKey();

//            // Assert
//            Assert.IsNotNull(key);
//            Assert.AreEqual(1, key.Properties.Count);
//            Assert.AreEqual("Id", key.Properties[0].Name);
//        }
//    }
//}
