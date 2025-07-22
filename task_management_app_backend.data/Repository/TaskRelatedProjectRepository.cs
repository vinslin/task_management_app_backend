
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
namespace task_management_app_backend.data.Repository
{
    public class TaskRelatedProjectRepository : ITaskRelatedProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRelatedProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(TaskRelatedProject task)
        {
            var task1 = new TaskRelatedProject
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
            };
            await _context.TaskRelatedProjects.AddAsync(task1);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskRelatedProject> UpdateAsync(TaskRelatedProject task)
        {
            var result = _context.TaskRelatedProjects.Update(task);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<List<TaskRelatedProject>> GetAllAsync()
        {
            return await _context.TaskRelatedProjects.ToListAsync();
        }

        public async Task<bool> DeleteAsync(TaskRelatedProject relation)
        {
            _context.TaskRelatedProjects.Remove(relation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TaskRelatedProject>> ProjectReleatedTasksAsync(Guid id)
        {
            return await _context.TaskRelatedProjects
                .Where(pt => pt.ProjectId == id)
                .Include(pt => pt.Task)
                .ToListAsync();
        }
    }
}
