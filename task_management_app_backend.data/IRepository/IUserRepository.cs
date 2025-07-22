
using task_management_app_backend.resources.Entities;

namespace task_management_app_backend.data.IRepository
{
    public interface IUserRepository
    {
        public Task<bool> AddAsync(User user);

        public Task<User> GetUserAsync(string email);

        
    }
}
