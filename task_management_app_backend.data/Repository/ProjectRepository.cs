
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.resources.Dtos.RequestDto;

namespace task_management_app_backend.data.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Project Add(CreateProjectDto projectDto)
        {
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = projectDto.ProjectName,
                Description = projectDto.Description,
                CreatedAt = DateTime.UtcNow,
            };
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

            return result.Entity;
        }
        public Project GetProjectById(Guid id)
        {
            return _context.Projects.FirstOrDefault(p => p.Id == id);
        }
       
    }
}
