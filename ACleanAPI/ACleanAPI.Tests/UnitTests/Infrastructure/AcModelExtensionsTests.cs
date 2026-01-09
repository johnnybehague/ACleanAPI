using ACleanAPI.Tests.App;
using ACleanAPI.Infrastructure.Core;

namespace ACleanAPI.Tests.UnitTests.Infrastructure;

[TestClass]
public class AcModelExtensionsTests
{
    [TestMethod]
    public void UpdateFrom_UpdatesAllPropertiesExceptId()
    {
        // Arrange
        var target = new UserTestModel { Id = 1, FirstName = "Alice", LastName = "Wonder", Email = "alice@wonder.com" };
        var source = new UserTestModel { Id = 2, FirstName = "Bob", LastName = "Rob", Email = "bob@rob.com" };

        // Act
        target.UpdateFrom(source);

        // Assert
        Assert.AreEqual(1, target.Id, "Id should not be updated");
        Assert.AreEqual("Bob", target.FirstName);
        Assert.AreEqual("Rob", target.LastName);
        Assert.AreEqual("bob@rob.com", target.Email);
    }

    [TestMethod]
    public void UpdateFrom_ThrowsArgumentNullException_WhenSourceIsNull()
    {
        // Arrange
        var target = new UserTestModel { Id = 1, FirstName = "Alice", LastName = "Wonder", Email = "alice@wonder.com" };
        UserTestModel? source = null;

        // Act & Assert
        Assert.ThrowsException<ArgumentNullException>(() => target.UpdateFrom(source));
    }
}
