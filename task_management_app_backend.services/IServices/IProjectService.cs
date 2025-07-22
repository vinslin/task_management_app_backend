using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using task_management_app_backend.resources.Dtos.RequestDto;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.services.IServices
{
    public interface IProjectService
    {
        Task<Project> AddProjectAsync(CreateProjectDto projectDto);
        Task<List<Project>> GetAllProjectsAsync();
        Task<Project> UpdateProjectAsync(Guid id, CreateProjectDto project);
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<bool> DeleteProjectAsync(Guid id);
        Task<List<getProjectsScrollbarDto>> GetProjectScrollAsync();
        Task<Project> GetOneProjectAsync(Guid id);
    }
}
