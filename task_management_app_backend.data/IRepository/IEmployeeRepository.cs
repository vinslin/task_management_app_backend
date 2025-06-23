using System;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.data.IRepository
{
    public interface IEmployeeRepository
    {
        public Employee AddEmployee(CreateEmployeeDto employeeDto);

        public List<Employee> GetAll();

        public Employee Update(Employee employee);

        public Employee GetElementById(Guid id);
    }
}
