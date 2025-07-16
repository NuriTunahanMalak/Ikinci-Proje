using Microsoft.EntityFrameworkCore;
using SirketBusiness.Interfaces;
using SirketData;
using SirketEntites;

namespace SirketBusiness.Services
{
    public class ProjectService : IProjectService
    {
        private readonly SirketDbContext _context;

        public ProjectService(SirketDbContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects
                .Include(p => p.ProjectProducts) // ürün ilişkilerini de çek
                .Include(p => p.UserProjects)    // kullanıcı ilişkilerini de çek
                .ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            return await _context.Projects
                .Include(p => p.ProjectProducts)
                .Include(p => p.UserProjects)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(Project project)
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }
    }
}
