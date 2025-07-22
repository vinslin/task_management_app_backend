
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.data.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Entities.Task> AddAsync(Entities.Task task)
        {
            var task1 = new Entities.Task
            {
                ID = Guid.NewGuid(),
                Title = task.Title,
                Description = task.Description,
                CreatedAt = DateTime.UtcNow,
                Priority = task.Priority,
                DueDate = task.DueDate,
            };
            await _context.Tasks.AddAsync(task1);
            await _context.SaveChangesAsync();
            return task1;
        }

        public async Task<List<Entities.Task>> GetAllAsync()
        {
            return await _context.Tasks
                .Include(t => t.TaskProjects)
                    .ThenInclude(tp => tp.Project)
                .Include(t => t.UserTasks)
                    .ThenInclude(ut => ut.Employee)
                .ToListAsync();
        }

        public async Task<Entities.Task?> GetElementByIdAsync(Guid id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task<Entities.Task> UpdateAsync(Entities.Task task)
        {
            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task> DeleteAsync(Entities.Task task)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Entities.Task?> GetOneAsync(Guid id)
        {
            return await _context.Tasks
                .Include(t => t.TaskProjects)
                    .ThenInclude(tp => tp.Project)
                .Include(t => t.UserTasks)
                    .ThenInclude(ut => ut.Employee)
                .FirstOrDefaultAsync(t => t.ID == id);
        }
    }
}
