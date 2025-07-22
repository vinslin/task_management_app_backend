using System;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.data.IRepository
{
    public interface IEmployeeRepository
    {
        Task<Employee> AddEmployeeAsync(Employee employee);

        Task<List<Employee>> GetAllAsync();

        Task<Employee> UpdateAsync(Employee employee);

        Task<Employee> GetElementByIdAsync(Guid id);

        Task<bool> DeleteEmployeeAsync(Guid id);

        Task<List<getEmployeeScrollBarDto>> GetEmpScrollAsync();
    }
}
