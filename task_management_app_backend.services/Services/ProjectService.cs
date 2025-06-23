
using task_management_app_backend.data.Data;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.data.Repository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.services.IServices;


namespace task_management_app_backend.services.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public Project AddProject(CreateProjectDto projectDto)
        {
            return _projectRepository.Add(projectDto);
        }
        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }
        public Project UpdateProject(Guid id,CreateProjectDto dto)
        {

            var update = _projectRepository.GetProjectById(id);
            if (update == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }
            else
            {
                update.Name = dto.ProjectName;
                update.Description = dto.Description;
               
                update.UpdatedAt = DateTime.UtcNow;
            }

            return _projectRepository.Update(update);
        }
        public Project GetProjectById(Guid id)
        {
            return _projectRepository.GetProjectById(id);
        }
    }
    
}
