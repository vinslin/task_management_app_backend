

namespace task_management_app_backend.data.IRepository
{
    public interface ITaskRepository
    {
        public Entities.Task Add(Entities.Task task);

        public List<Entities.Task> GetAll();

    //    public Entities.Task Update(Entities.Task task);

        public Entities.Task GetElementById(Guid id);

        data.Entities.Task Update(data.Entities.Task task);
        Entities.Task Delete(Entities.Task task);


    }
}
