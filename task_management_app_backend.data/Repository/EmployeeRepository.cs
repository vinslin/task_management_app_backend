
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Employee AddEmployee(CreateEmployeeDto employeeDto)
        {

            var employee = new Employee
            {
                ID = Guid.NewGuid(),
                Name = employeeDto.UserName,
                Email = employeeDto.Email,
                Role = employeeDto.Role,
                CreatedAt = DateTime.UtcNow,

            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee Update(Employee employee) {
           var result= _context.Employees.Update(employee);
               
            return result.Entity;
        }
        public Employee GetElementById(Guid id)
        {
            return _context.Employees
                .Include(e => e.UserTasks)
                    .ThenInclude(ut => ut.Task)
                .FirstOrDefault(e => e.ID == id);
        }

    }
}