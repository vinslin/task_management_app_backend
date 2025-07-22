
using task_management_app_backend.data.Entities;


namespace task_management_app_backend.data.IRepository
{
    public interface  ITaskRelatedProjectRepository
    {
        Task<bool> AddAsync(TaskRelatedProject task);
        Task<TaskRelatedProject> UpdateAsync(TaskRelatedProject task);
        // Additional methods can be added as needed, such as GetById, GetAll, etc.

        Task<List<TaskRelatedProject>> GetAllAsync();
        Task<bool> DeleteAsync(TaskRelatedProject relation);

        Task<List<TaskRelatedProject>> ProjectReleatedTasksAsync(Guid id);
    }
}
