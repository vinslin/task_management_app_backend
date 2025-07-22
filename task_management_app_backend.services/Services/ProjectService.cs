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

        public async Task<Project> AddProjectAsync(CreateProjectDto projectDto)
        {
            var project = new Project
            {
                Name = projectDto.ProjectName,
                Description = projectDto.Description
                // CreatedAt is handled in the repository
            };

            return await _projectRepository.AddAsync(project);
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project> UpdateProjectAsync(Guid id, CreateProjectDto dto)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            project.Name = dto.ProjectName;
            project.Description = dto.Description;
            project.UpdatedAt = DateTime.UtcNow;

            return await _projectRepository.UpdateAsync(project);
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetProjectByIdAsync(id);
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            return await _projectRepository.DeleteProAsync(id);
        }

        public async Task<List<getProjectsScrollbarDto>> GetProjectScrollAsync()
        {
            return await _projectRepository.GetProScrollAsync();
        }

        public async Task<Project> GetOneProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            if (project == null)
            {
                throw new KeyNotFoundException($"Project with ID {id} not found.");
            }

            return project;
        }       
    }
}
