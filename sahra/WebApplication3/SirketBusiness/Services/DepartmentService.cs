using Microsoft.EntityFrameworkCore;
using SirketBusiness.Interfaces;
using SirketData;
using SirketEntites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirketBusiness.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly SirketDbContext _context;

        public DepartmentService(SirketDbContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.ToListAsync();
        }

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _context.Departments.FindAsync(id);
        }

        public async Task AddAsync(Department department)
        {
            await _context.Departments.AddAsync(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Departments.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var dep = await _context.Departments.FindAsync(id);
            if (dep != null)
            {
                _context.Departments.Remove(dep);
                await _context.SaveChangesAsync();
            }
        }
    }
}
