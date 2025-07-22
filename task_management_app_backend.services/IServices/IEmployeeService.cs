using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.IServices
{
    public interface IEmployeeService
    {
        Task<Employee> AddEmployeeAsync(CreateEmployeeDto createDto);

        Task<List<Employee>> GetAllEmployeeAsync();

        Task<Employee> UpdateEmployeeAsync(Guid id, CreateEmployeeDto dto);

        Task<List<ResponseCreateTaskDto>> GetEmployeeTasksAsync(Guid id);

        Task<bool> DeleteEmployeeAsync(Guid id);

        Task<List<getEmployeeScrollBarDto>> GetEmployeeScrollAsync();

        Task<Employee> GetEmployeeAsync(Guid id);
    }
}
