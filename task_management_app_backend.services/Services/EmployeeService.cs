
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;
using task_management_app_backend.data.IRepository;


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
             return _employeeRepository.AddEmployee(createDto);

        }

        public  List<Employee> GetAllEmployee()
        { 
             return _employeeRepository.GetAll();
        }
        public Employee UpdateEmployee(Guid id,CreateEmployeeDto dto) { 

            var employee= _employeeRepository.GetElementById(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            else {
                employee.Name = dto.UserName;
                employee.Email = dto.Email;
                employee.Role = dto.Role;
                employee.UpdatedAt = DateTime.UtcNow;
            }

                return _employeeRepository.Update(employee);
        }

    }
}
