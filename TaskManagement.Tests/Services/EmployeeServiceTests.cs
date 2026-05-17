using AutoMapper;
using FluentAssertions;
using Moq;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
using task_management_app_backend.services.Services;
using Xunit;

namespace TaskManagement.Tests.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IEmployeeRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _repositoryMock = new Mock<IEmployeeRepository>();

            //automappr pandrathu 
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateEmployeeDto, Employee>();

            });
            _mapper = config.CreateMapper();

            _service = new EmployeeService(
    _repositoryMock.Object,
    _mapper
);

        }

        [Fact]
        public async System.Threading.Tasks.Task AddEmployeeAsync_ShouldAddEmployee()
        {
            // Arrange
            var dto = new CreateEmployeeDto
            {
                UserName = "John",
                Email = "john@test.com",
                Role = "Developer"
            };

            _repositoryMock
                .Setup(x => x.AddEmployeeAsync(It.IsAny<Employee>()))
                .ReturnsAsync((Employee emp) => emp);

            // Act
            var result = await _service.AddEmployeeAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.UserName);
            result.Email.Should().Be(dto.Email);
            result.Role.Should().Be(dto.Role);

            _repositoryMock.Verify(
                x => x.AddEmployeeAsync(It.IsAny<Employee>()),
                Times.Once
            );
        }




        [Fact]
        public async System.Threading.Tasks.Task GetEmployeeAsync_ShouldThrow_WhenEmployeeNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock
                .Setup(x => x.GetElementByIdAsync(id))
                .ReturnsAsync((Employee)null);

            // Act
            Func<System.Threading.Tasks.Task> act = async () =>
                await _service.GetEmployeeAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>()
                .WithMessage($"Employee with ID {id} not found.");
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateEmployeeAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var id = Guid.NewGuid();

            var employee = new Employee
            {
                ID = id,
                Name = "Old Name",
                Email = "old@test.com",
                Role = "Old Role"
            };

            var dto = new CreateEmployeeDto
            {
                UserName = "New Name",
                Email = "new@test.com",
                Role = "Developer"
            };

            _repositoryMock
                .Setup(x => x.GetElementByIdAsync(id))
                .ReturnsAsync(employee);

            _repositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Employee>()))
                .ReturnsAsync((Employee emp) => emp);

            // Act
            var result = await _service.UpdateEmployeeAsync(id, dto);

            // Assert
            result.Name.Should().Be(dto.UserName);
            result.Email.Should().Be(dto.Email);
            result.Role.Should().Be(dto.Role);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateEmployeeAsync_ShouldThrow_WhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            var dto = new CreateEmployeeDto();

            _repositoryMock
                .Setup(x => x.GetElementByIdAsync(id))
                .ReturnsAsync((Employee)null);

            // Act
            Func<System.Threading.Tasks.Task> act = async () =>
                await _service.UpdateEmployeeAsync(id, dto);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteEmployeeAsync_ShouldReturnTrue()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock
                .Setup(x => x.DeleteEmployeeAsync(id))
                .ReturnsAsync(true);

            // Act
            var result = await _service.DeleteEmployeeAsync(id);

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async System.Threading.Tasks.Task GetEmployeeScrollAsync_ShouldReturnScrollData()
        {
            // Arrange
            var scrollData = new List<getEmployeeScrollBarDto>
            {
                new getEmployeeScrollBarDto(),
                new getEmployeeScrollBarDto()
            };

            _repositoryMock
                .Setup(x => x.GetEmpScrollAsync())
                .ReturnsAsync(scrollData);

            // Act
            var result = await _service.GetEmployeeScrollAsync();

            // Assert
            result.Should().HaveCount(2);
        }



        [Fact]
        public async System.Threading.Tasks.Task GetEmployeeTasksAsync_ShouldThrow_WhenEmployeeNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();

            _repositoryMock
                .Setup(x => x.GetElementByIdAsync(id))
                .ReturnsAsync((Employee)null);

            // Act
            Func<System.Threading.Tasks.Task> act = async () =>
                await _service.GetEmployeeTasksAsync(id);

            // Assert
            await act.Should()
                .ThrowAsync<KeyNotFoundException>();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("invalid-email")]
        public async System.Threading.Tasks.Task AddEmployeeAsync_ShouldThrow_WhenEmailInvalid(string email)
        {
            // Arrange
            var dto = new CreateEmployeeDto
            {
                UserName = "John",
                Email = email,
                Role = "Developer"
            };
            // Act
            Func<System.Threading.Tasks.Task> act = async () =>
                await _service.AddEmployeeAsync(dto);
            // Assert
            await act.Should()
                .ThrowAsync<Exception>();



            //to check 3 times execution pandrathu
            //_repositoryMock.Verify(
            //    x=> x.AddEmployeeAsync(It.IsAny<Employee>()),
            //    Times.Exactly(3));
        }
        [Theory]
        [InlineData("John", "john@test.com", "Admin")]
        [InlineData("Jane", "jane@test.com", "Manager")]
        public async System.Threading.Tasks.Task AddEmployeeAsync_ShouldPassCorrectValues( string name,string email,string role)
        {
            // Arrange
            var dto = new CreateEmployeeDto
            {
                UserName = name,
                Email = email,
                Role = role
            };

            _repositoryMock
                .Setup(x => x.AddEmployeeAsync(It.IsAny<Employee>()))
                .ReturnsAsync((Employee e) => e);

            // Act
            await _service.AddEmployeeAsync(dto);

            // Assert
            _repositoryMock.Verify(
                x => x.AddEmployeeAsync(
                    It.Is<Employee>(e =>
                        e.Name == name &&
                        e.Email == email &&
                        e.Role == role)),
                Times.Once);
        }
    }
}
