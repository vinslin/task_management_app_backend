using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using System.Linq;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;

namespace task_management_app_backend.GraphQL.Queries
{
    public class EmployeeQuery
    {
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        [UseProjection]
        public IQueryable<Employee> GetEmployees([Service] ApplicationDbContext context)
        {
            return context.Employees;
        }

        public Employee? GetEmployeeById(Guid id, [Service] ApplicationDbContext context)
        {
            return context.Employees.FirstOrDefault(e => e.ID == id);
        }
    }
}
