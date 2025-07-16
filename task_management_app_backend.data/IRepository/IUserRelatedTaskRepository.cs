using task_management_app_backend.resources.Dtos.MiddleDto;
using task_management_app_backend.data.Entities;

namespace task_management_app_backend.data.IRepository
{
    public interface IUserRelatedTaskRepository
    {

        public bool Add(UserReleatedTask task);
        public UserReleatedTask Update(UserReleatedTask task);

        public List<UserReleatedTask> GetAll();

        public bool Delete(UserReleatedTask relation);

        public List<UserReleatedTask> employeeReleatedTasks(Guid id);
     


    }
}
