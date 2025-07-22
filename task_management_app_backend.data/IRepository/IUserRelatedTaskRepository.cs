using task_management_app_backend.resources.Dtos.MiddleDto;
using task_management_app_backend.data.Entities;

namespace task_management_app_backend.data.IRepository
{
    public interface IUserRelatedTaskRepository
    {

        public Task<bool> AddAsync(UserReleatedTask task);
        public Task<UserReleatedTask> UpdateAsync(UserReleatedTask task);

        public Task<List<UserReleatedTask>> GetAllAsync();

        public Task<bool> DeleteAsync(UserReleatedTask relation);

        public Task<List<UserReleatedTask>> EmployeeReleatedTasksAsync(Guid id);
     


    }
}
