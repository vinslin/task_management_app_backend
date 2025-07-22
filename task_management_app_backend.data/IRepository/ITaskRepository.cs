

namespace task_management_app_backend.data.IRepository
{
    public interface ITaskRepository
    {
        public Task<Entities.Task> AddAsync(Entities.Task task);

        public Task<List<Entities.Task>> GetAllAsync();

    //    public Entities.Task Update(Entities.Task task);

        public Task<Entities.Task> GetElementByIdAsync(Guid id);

       public Task< data.Entities.Task> UpdateAsync(data.Entities.Task task);
       public Task< Entities.Task> DeleteAsync(Entities.Task task);

        public Task<Entities.Task?> GetOneAsync(Guid id);


    }
}
