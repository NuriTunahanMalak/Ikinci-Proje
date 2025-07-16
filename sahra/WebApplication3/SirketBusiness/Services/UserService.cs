using SirketBusiness.Interfaces;
using SirketData;
using SirketEntites;
using Microsoft.EntityFrameworkCore;

namespace SirketBusiness.Services
{
    public class UserService : IUserService
    {
        private readonly SirketDbContext _context;

        public UserService(SirketDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
            public async Task<List<User>> GetUsersByDepartmentAsync(int departmentId)
        {
            return await _context.Users
                .Where(u => u.DepartmentId == departmentId)
                .ToListAsync();
        }

    }
}

