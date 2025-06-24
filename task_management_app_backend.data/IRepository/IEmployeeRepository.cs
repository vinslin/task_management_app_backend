using task_management_app_backend.data.Entities;
using System;

namespace task_management_app_backend.data.IRepository
{
    public interface IEmployeeRepository
    {
        Employee AddEmployee(Employee employee); // Removed CreateEmployeeDto

        List<Employee> GetAll();

        Employee Update(Employee employee);

        Employee GetElementById(Guid id);
    }
}
