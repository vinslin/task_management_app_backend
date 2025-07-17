
using task_management_app_backend.data.Entities;


namespace task_management_app_backend.data.IRepository
{
    public interface ITaskRelatedProjectRepository
    {
        public bool Add(TaskRelatedProject task);
        public TaskRelatedProject Update(TaskRelatedProject task);
        // Additional methods can be added as needed, such as GetById, GetAll, etc.

        public List<TaskRelatedProject> GetAll();
        public bool Delete(TaskRelatedProject relation);

        public List<TaskRelatedProject> projectReleatedTasks(Guid id);
    }
}
