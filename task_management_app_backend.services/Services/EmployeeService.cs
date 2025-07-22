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

            public async Task<Employee> AddEmployeeAsync(CreateEmployeeDto createDto)
            {
                var employee = _mapper.Map<Employee>(createDto);
                employee.ID = Guid.NewGuid();
                employee.CreatedAt = DateTime.UtcNow;
                employee.UpdatedAt = DateTime.UtcNow;

                return await _employeeRepository.AddEmployeeAsync(employee);
            }

            public async Task<List<Employee>> GetAllEmployeeAsync()
            {
                return await _employeeRepository.GetAllAsync();
            }

            public async Task<Employee> UpdateEmployeeAsync(Guid id, CreateEmployeeDto dto)
            {
                var employee = await _employeeRepository.GetElementByIdAsync(id);
                if (employee == null)
                    throw new KeyNotFoundException($"Employee with ID {id} not found.");

                employee.Name = dto.UserName;
                employee.Email = dto.Email;
                employee.Role = dto.Role;
                employee.UpdatedAt = DateTime.UtcNow;

                return await _employeeRepository.UpdateAsync(employee);
            }

            public async Task<List<ResponseCreateTaskDto>> GetEmployeeTasksAsync(Guid id)
            {
                var employee = await _employeeRepository.GetElementByIdAsync(id);
                if (employee == null)
                    throw new KeyNotFoundException($"Employee with ID {id} not found.");

                return _mapper.Map<List<ResponseCreateTaskDto>>(employee.UserTasks.Select(ut => ut.Task).ToList());
            }

            public async Task<bool> DeleteEmployeeAsync(Guid id)
            {
                return await _employeeRepository.DeleteEmployeeAsync(id);
            }

            public async Task<List<getEmployeeScrollBarDto>> GetEmployeeScrollAsync()
            {
                return await _employeeRepository.GetEmpScrollAsync();
            }

            public async Task<Employee> GetEmployeeAsync(Guid id)
            {
                var employee = await _employeeRepository.GetElementByIdAsync(id);
                if (employee == null)
                    throw new KeyNotFoundException($"Employee with ID {id} not found.");

                return employee;
            }
        }
    }
