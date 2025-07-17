
using task_management_app_backend.resources.Entities;

namespace task_management_app_backend.data.IRepository
{
    public interface IUserRepository
    {
        public bool Add(User user);

        public User GetUser(string email);

        
    }
}
