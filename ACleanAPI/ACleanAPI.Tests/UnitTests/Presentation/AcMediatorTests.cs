using ACleanAPI.Presentation;
using LiteBus.Commands.Abstractions;
using LiteBus.Queries.Abstractions;
using Moq;

namespace ACleanAPI.Tests.UnitTests.Presentation
{
    [TestClass]
    public class AcMediatorTests
    {
        private readonly Mock<IQueryMediator> _mockQueryMediator;
        private readonly Mock<ICommandMediator> _mockCommandMediator;
        private readonly AcMediator _acMediator;

        public AcMediatorTests()
        {
            _mockQueryMediator = new Mock<IQueryMediator>();
            _mockCommandMediator = new Mock<ICommandMediator>();
            _acMediator = new AcMediator(_mockQueryMediator.Object, _mockCommandMediator.Object);
        }

        [TestMethod]
        public async Task QueryAsync_WithCancellationToken_DelegatesToQueryMediator()
        {
            var query = new Mock<IQuery<string>>().Object;
            var expected = "result";
            _mockQueryMediator.Setup(m => m.QueryAsync(query, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _acMediator.QueryAsync(query);

            Assert.AreEqual(expected, result);
            _mockQueryMediator.Verify(m => m.QueryAsync(query, null, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task QueryAsync_WithSettings_DelegatesToQueryMediator()
        {
            var query = new Mock<IQuery<string>>().Object;
            var settings = new QueryMediationSettings();
            var expected = "result";
            _mockQueryMediator.Setup(m => m.QueryAsync(query, settings, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _acMediator.QueryAsync(query, settings);

            Assert.AreEqual(expected, result);
            _mockQueryMediator.Verify(m => m.QueryAsync(query, settings, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task SendAsync_Command_DelegatesToCommandMediator()
        {
            var command = new Mock<ICommand>().Object;
            _mockCommandMediator.Setup(m => m.SendAsync(command, null, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _acMediator.SendAsync(command);

            _mockCommandMediator.Verify(m => m.SendAsync(command, null, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task SendAsync_Command_WithSettings_DelegatesToCommandMediator()
        {
            var command = new Mock<ICommand>().Object;
            var settings = new CommandMediationSettings();
            _mockCommandMediator.Setup(m => m.SendAsync(command, settings, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _acMediator.SendAsync(command, settings);

            _mockCommandMediator.Verify(m => m.SendAsync(command, settings, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task SendAsync_GenericCommand_DelegatesToCommandMediator()
        {
            var command = new Mock<ICommand<string>>().Object;
            var expected = "result";
            _mockCommandMediator.Setup(m => m.SendAsync(command, null, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _acMediator.SendAsync(command);

            Assert.AreEqual(expected, result);
            _mockCommandMediator.Verify(m => m.SendAsync(command, null, It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task SendAsync_GenericCommand_WithSettings_DelegatesToCommandMediator()
        {
            var command = new Mock<ICommand<string>>().Object;
            var settings = new CommandMediationSettings();
            var expected = "result";
            _mockCommandMediator.Setup(m => m.SendAsync(command, settings, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var result = await _acMediator.SendAsync(command, settings);

            Assert.AreEqual(expected, result);
            _mockCommandMediator.Verify(m => m.SendAsync(command, settings, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
