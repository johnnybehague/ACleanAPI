using ACleanAPI.API.Controllers;
using ACleanAPI.Example.Application.Users.Commands;
using ACleanAPI.Example.Application.Users.DTO;
using ACleanAPI.Example.Application.Users.Queries;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ACleanAPI.Example.API.Tests
{
    [TestClass]
    public sealed class UserControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UserController(_mediatorMock.Object);
        }

        [TestMethod]
        public async Task Index_ReturnsListOfUserDto()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto { Id = 1, FirstName = "John", LastName = "Doe" },
                new UserDto { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok<IEnumerable<UserDto>>(users));

            // Act
            var response = await _controller.Index(CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            var okResult = response.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedUsers = okResult.Value as IEnumerable<UserDto>;
            Assert.IsNotNull(returnedUsers);
        }

        [TestMethod]
        public async Task Details_ReturnsUserDetailDto()
        {
            // Arrange
            var userDetail = new UserDetailDto { Id = 1, Email = "john@doe.com", FirstName = "John", LastName = "Doe" };

            _mediatorMock
                .Setup(m => m.Send(It.Is<GetUserByIdQuery>(q => q.Id == 1), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok(userDetail));

            // Act
            var response = await _controller.Details(1, CancellationToken.None);

            // Assert
            Assert.IsNotNull(response);
            var okResult = response.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedUser = okResult.Value as UserDetailDto;
            Assert.IsNotNull(returnedUser);
            Assert.AreEqual(1, returnedUser.Id);
        }

        [TestMethod]
        public async Task Create_ReturnsNoContent()
        {
            // Arrange
            UserDto dto = new UserDto { Id = 1, FirstName = "John", LastName = "Doe" };
            _mediatorMock.Setup(m => m.Send(It.Is<CreateUserCommand>(q => q.Dto == dto), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            // Act
            var result = await _controller.Create(dto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NoContentResult>(result);
        }

        [TestMethod]
        public async Task Update_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            UserDto dto = new UserDto { Id = 1, FirstName = "John", LastName = "Doe" };
            _mediatorMock.Setup(m => m.Send(It.Is<UpdateUserCommand>(q => q.Id == id && q.Dto == dto), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            // Act
            var result = await _controller.Update(id, dto, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NoContentResult>(result);
        }

        [TestMethod]
        public async Task Delete_ReturnsNoContent()
        {
            // Arrange
            int id = 1;
            _mediatorMock.Setup(m => m.Send(It.Is<DeleteUserCommand>(q => q.Id == id), It.IsAny<CancellationToken>()))
                .ReturnsAsync(Result.Ok());

            // Act
            var result = await _controller.Delete(id, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType<NoContentResult>(result);
        }
    }
}
