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

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            employee.ID = Guid.NewGuid();
            employee.CreatedAt = DateTime.UtcNow;

            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateAsync(Employee employee)
        {
            var result = _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Employee?> GetElementByIdAsync(Guid id)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ID == id);

            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<getEmployeeScrollBarDto>> GetEmpScrollAsync()
        {
            return await _context.Employees
               .Select(e => new getEmployeeScrollBarDto
               {
                   Id = e.ID,
                   Name = e.Name
               })
               .ToListAsync();
        }
    }   
}
