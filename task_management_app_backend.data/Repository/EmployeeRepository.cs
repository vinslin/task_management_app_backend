using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Employee AddEmployee(Employee employee)
        {
            employee.ID = Guid.NewGuid();
            employee.CreatedAt = DateTime.UtcNow;

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return employee;
        }

        public List<Employee> GetAll()
        {
            return _context.Employees.ToList();
        }

        public Employee Update(Employee employee)
        {
            var result = _context.Employees.Update(employee);
            _context.SaveChanges();
            return result.Entity;
        }

        public Employee GetElementById(Guid id)
        {
            return _context.Employees.FirstOrDefault(e => e.ID == id);
        }



        public bool DeleteEmployee(Guid id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.ID == id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }

        public List<getEmployeeScrollBarDto> getEmpScroll() {

            return _context.Employees
               .Select(e => new getEmployeeScrollBarDto
               {
                   Id = e.ID,
                   Name = e.Name
               })
               .ToList();


        }

    }
}
