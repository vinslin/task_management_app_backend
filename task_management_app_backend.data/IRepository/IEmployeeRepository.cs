using System;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.data.IRepository
{
    public interface IEmployeeRepository
    {
        Employee AddEmployee(Employee employee); 

        List<Employee> GetAll();

        Employee Update(Employee employee);

        Employee GetElementById(Guid id);

        bool DeleteEmployee(Guid id);

        List<getEmployeeScrollBarDto> getEmpScroll();
    }
}
