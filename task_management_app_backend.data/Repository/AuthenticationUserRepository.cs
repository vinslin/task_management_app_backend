
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.resources.Entities;
using task_management_app_backend.data.IRepository;
using AutoMapper.Configuration.Annotations;

namespace task_management_app_backend.data.Repository
{
    public  class AuthenticationUserRepository :IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthenticationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(User user)
        {
            try
            {
                _context.Users.Add(user);

                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return false;
            }
        }

        public User GetUser(string email)
        {
          
                return _context.Users.FirstOrDefault(u =>  u.Email == email);
            
      
        }
    }
}
