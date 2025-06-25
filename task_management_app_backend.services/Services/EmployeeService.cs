using AutoMapper;
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
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public Employee AddEmployeeAsync(CreateEmployeeDto createDto)
        {
            var employee = _mapper.Map<Employee>(createDto);
            employee.ID = Guid.NewGuid();
            employee.CreatedAt = DateTime.UtcNow;
            employee.UpdatedAt = DateTime.UtcNow;

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
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            // You can also do _mapper.Map(dto, employee) if you prefer full mapping
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
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            return _mapper.Map<List<ResponseCreateTaskDto>>(employee.UserTasks.Select(ut => ut.Task).ToList());
        }
    }
}
