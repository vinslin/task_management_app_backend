
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
        public Entities.Task Add(Entities.Task task)
        {
            var task1 = new Entities.Task
            {
                ID = Guid.NewGuid(),
                Title = task.Title,
                Description = task.Description,
                CreatedAt = DateTime.UtcNow,
                Priority  = task.Priority,
                DueDate   = task.DueDate,
            };
            _context.Tasks.Add(task1);
            _context.SaveChanges();
            return task1;
        }
        public List<Entities.Task> GetAll()
        {
            return _context.Tasks
                .Include(t => t.TaskProjects)
                    .ThenInclude(tp => tp.Project)
                .Include(t => t.UserTasks)
                    .ThenInclude(ut => ut.Employee)
                .ToList();
        }

        public Entities.Task Update(Entities.Task task)
        {
            var result = _context.Tasks.Update(task);
            _context.SaveChanges();
            return result.Entity;
        }
        public Entities.Task GetElementById(Guid id)
        {
            return _context.Tasks.FirstOrDefault(p => p.ID == id);
        }
    }
}
