
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
        public bool Add(TaskRelatedProject task)
        {
            var task1 = new TaskRelatedProject
            {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,
         
            };
            _context.TaskRelatedProjects.Add(task1);
            _context.SaveChanges();
            return true;
        }
        public TaskRelatedProject Update(TaskRelatedProject task)
        {
            var result = _context.TaskRelatedProjects.Update(task);

            return result.Entity;
        }
    }
}
