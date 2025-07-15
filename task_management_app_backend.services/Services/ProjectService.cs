using System.Reflection.Metadata.Ecma335;
using task_management_app_backend.data.Entities;
using task_management_app_backend.data.IRepository;
using task_management_app_backend.data.Repository;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;
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
            var project = new Project
            {
                Name = projectDto.ProjectName,
                Description = projectDto.Description
                // CreatedAt is handled in the repository
            };

            return _projectRepository.Add(project);
        }

        public List<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }

        public Project UpdateProject(Guid id, CreateProjectDto dto)
        {
            var project = _projectRepository.GetProjectById(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            project.Name = dto.ProjectName;
            project.Description = dto.Description;
            project.UpdatedAt = DateTime.UtcNow;

            return _projectRepository.Update(project);
        }

        public Project GetProjectById(Guid id)
        {
            return _projectRepository.GetProjectById(id);
        }

        public bool deleteProject(Guid id) {

            var project = _projectRepository.GetProjectById(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            return _projectRepository.DeletePro(id);        
        }

        public List<getProjectsScrollbarDto> getProjectScroll() {

            return _projectRepository.getProScroll();
        }
        public Project getOneProject(Guid id) {

            var project = _projectRepository.GetProjectById(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            return project;
        }
    }
}
