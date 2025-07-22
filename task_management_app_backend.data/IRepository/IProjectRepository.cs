using task_management_app_backend.data.Entities;
using task_management_app_backend.resources.Dtos.ResponseDto;

namespace task_management_app_backend.data.IRepository
{
    public interface IProjectRepository
    {
        Task<Project> AddAsync(Project project);
        Task<List<Project>> GetAllAsync();
        Task<Project> UpdateAsync(Project project);
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<bool> DeleteProAsync(Guid id);
        Task<List<getProjectsScrollbarDto>> GetProScrollAsync();
    }
}
