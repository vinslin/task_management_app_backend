using HotChocolate;
using HotChocolate.Types;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;

namespace task_management_app_backend.GraphQL.Types
{
    public class EmployeeType : ObjectType<Employee>
    {
        protected override void Configure(IObjectTypeDescriptor<Employee> descriptor)
        {
            descriptor.Field(e => e.ID).Type<NonNullType<UuidType>>();
            descriptor.Field(e => e.Name).Type<NonNullType<StringType>>();
            descriptor.Field(e => e.Email).Type<StringType>();
            descriptor.Field(e => e.Role).Type<StringType>();

            // Automatically expose related tasks
            descriptor.Field(e => e.UserTasks).ResolveWith<EmployeeResolvers>(r => r.GetTasks(default!, default!));
        }

        private class EmployeeResolvers
        {
            public IEnumerable<UserReleatedTask> GetTasks(Employee employee, [Service] ApplicationDbContext context)
            {
                return context.userReleatedTasks.Where(ut => ut.EmployeeId == employee.ID).ToList();
            }
        }
    }
}
