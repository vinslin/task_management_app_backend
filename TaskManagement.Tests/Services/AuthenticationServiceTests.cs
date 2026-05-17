using Microsoft.Extensions.Configuration;
using FluentAssertions;
using Moq;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Entities;
using task_management_app_backend.services.Services;
using Xunit;
using System.Linq.Expressions;

namespace TaskManagement.Tests.Services
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly AuthenticationService _authService;

        public AuthenticationServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _configurationMock = new Mock<IConfiguration>();

            //mock jwt config values
            _configurationMock.Setup(x => x["Jwt:Key"])
                .Returns("my_secret_keahefkjshfekjhaskjhgfksdh908309hndasy");

            _configurationMock.Setup(x => x["Jwt:Issuer"])
                .Returns("TestIssuer");

            _authService = new AuthenticationService(_configurationMock.Object,_userRepositoryMock.Object);

        }

        [Fact]
        public async Task RegisterAsync_ShouldReturnTrue_WhenUserDoesNotExist()
        {
            //Arrange
            var newUser = new CreateUserDto
            {
                userName="John doe",
                email = "john@test.com",
                passWord ="Password123",
                role ="Manager"
            };

            _userRepositoryMock.Setup(x => x.GetUserAsync(newUser.email))
                .ReturnsAsync((User)null);

            _userRepositoryMock.Setup(x => x.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            //Act 
            var result = await _authService.RegisterAsync(newUser);

            //Assert
            result.Should().BeTrue();

            _userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task LoginAsync_ShouldReturnToken_WhenCredentialsAreValid()
        {
            //Arrange
            var passWord = "Password123";

            var user = new User
            {
                UserId = Guid.NewGuid(),
                UserName ="John Doe",
                Email = "john@test.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(passWord),
                Role = "Manager"
            };

            var loginRequest = new LoginRequestDto
            {
                Email = "john@test.com",
                Password = passWord
            };

            _userRepositoryMock.Setup(x => x.GetUserAsync(loginRequest.Email))
                .ReturnsAsync(user);

            var result = await _authService.LoginAsync(loginRequest);

            //Assert
            result.Should().NotBeNull();
            result.Token.Should().NotBeNullOrEmpty();
            result.email.Should().Be(user.Email);
            result.userName.Should().Be(user.UserName);
            result.role.Should().Be(user.Role);

        }

        [Fact]
        public async Task LoginAsync_ShouldThrowException_WhenUserDoesNotExist()
        {
            //Arrange
            var loginRequest = new LoginRequestDto
            {
                Email = "unknown@test.com",
                Password = "Password123"
            };

            _userRepositoryMock.Setup(x => x.GetUserAsync(loginRequest.Email))
                .ReturnsAsync((User)null);

            // Act
            Func<Task> act = async () => await _authService.LoginAsync(loginRequest);

            //Assert 
            await act.Should()
                .ThrowAsync<UnauthorizedAccessException>()
                .WithMessage("Invalid email or password.");
        }
    }
}
