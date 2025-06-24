using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.data.IRepository;
namespace task_management_app_backend.data.Repository
{
    public class UserRelatedTaskRepository : IUserRelatedTaskRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRelatedTaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(UserReleatedTask task)
        {
            var task1 = new UserReleatedTask
            {
                EmployeeId = task.EmployeeId,
                TaskId = task.TaskId,

            };
            _context.userReleatedTasks.Add(task1);
            _context.SaveChanges();
            return true;
        }
        public UserReleatedTask Update(UserReleatedTask task)
        {
            var result = _context.userReleatedTasks.Update(task);

            return result.Entity;
        }
    }
}
