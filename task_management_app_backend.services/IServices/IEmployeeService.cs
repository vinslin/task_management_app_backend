using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.IServices
{
    public interface IEmployeeService
    {
        Employee AddEmployeeAsync(CreateEmployeeDto createDto);

        List<Employee> GetAllEmployee();

        Employee UpdateEmployee(Guid id, CreateEmployeeDto dto);

        List<ResponseCreateTaskDto> GetEmployeeTasks(Guid id);
    }
}
