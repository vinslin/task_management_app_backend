    using Microsoft.EntityFrameworkCore;
    using task_management_app_backend.data.Data;
    using task_management_app_backend.data.Entities;

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

            public async Task<bool> AddAsync(UserReleatedTask task)
            {
                var task1 = new UserReleatedTask
                {
                    EmployeeId = task.EmployeeId,
                    TaskId = task.TaskId,
                };
                await _context.userReleatedTasks.AddAsync(task1);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<UserReleatedTask> UpdateAsync(UserReleatedTask task)
            {
                var result = _context.userReleatedTasks.Update(task);
                await _context.SaveChangesAsync();
                return result.Entity;
            }

            public async Task<List<UserReleatedTask>> GetAllAsync()
            {
                return await _context.userReleatedTasks.ToListAsync();
            }

            public async Task<bool> DeleteAsync(UserReleatedTask relation)
            {
                _context.userReleatedTasks.Remove(relation);
                await _context.SaveChangesAsync();
                return true;
            }

            public async Task<List<UserReleatedTask>> EmployeeReleatedTasksAsync(Guid id)
            {
                return await _context.userReleatedTasks
                    .Where(ut => ut.EmployeeId == id)
                    .Include(ut => ut.Task)
                    .ToListAsync();
            }
        }
    }
