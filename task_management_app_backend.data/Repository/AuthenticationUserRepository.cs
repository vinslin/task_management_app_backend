
using Microsoft.EntityFrameworkCore;
using task_management_app_backend.data.Data;
using task_management_app_backend.resources.Entities;
using task_management_app_backend.data.IRepository;
using AutoMapper.Configuration.Annotations;

namespace task_management_app_backend.data.Repository
{
    public class AuthenticationUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthenticationUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return false;
            }
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
