using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;

namespace task_management_app_backend.data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Project Add(Project project)
        {
            project.Id = Guid.NewGuid();
            project.CreatedAt = DateTime.UtcNow;

            _context.Projects.Add(project);
            _context.SaveChanges();
            return project;
        }

        public List<Project> GetAll()
        {
            return _context.Projects.ToList();
        }

        public Project Update(Project project)
        {
            var result = _context.Projects.Update(project);
            _context.SaveChanges();
            return result.Entity;
        }

        public Project GetProjectById(Guid id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }
    }
}
