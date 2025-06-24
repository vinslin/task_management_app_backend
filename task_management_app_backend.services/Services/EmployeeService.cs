using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
using task_management_app_backend.services.IServices;

namespace task_management_app_backend.services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public Employee AddEmployeeAsync(CreateEmployeeDto createDto)
        {
            var employee = new Employee
            {
                Name = createDto.UserName,
                Email = createDto.Email,
                Role = createDto.Role
                // ID and CreatedAt will be handled in the repository
            };

            return _employeeRepository.AddEmployee(employee);
        }

        public List<Employee> GetAllEmployee()
        {
            return _employeeRepository.GetAll();
        }

        public Employee UpdateEmployee(Guid id, CreateEmployeeDto dto)
        {
            var employee = _employeeRepository.GetElementById(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            employee.Name = dto.UserName;
            employee.Email = dto.Email;
            employee.Role = dto.Role;
            employee.UpdatedAt = DateTime.UtcNow;

            return _employeeRepository.Update(employee);
        }

        public List<ResponseCreateTaskDto> GetEmployeeTasks(Guid id)
        {
            var employee = _employeeRepository.GetElementById(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }

            return employee.UserTasks.Select(ut => new ResponseCreateTaskDto
            {
                TaskId = ut.Task.ID,
                Title = ut.Task.Title,
                Description = ut.Task.Description,
                Priority = ut.Task.Priority,
                DueDate = ut.Task.DueDate,
                EmployeeId = id,
            }).ToList();
        }
    }
}
