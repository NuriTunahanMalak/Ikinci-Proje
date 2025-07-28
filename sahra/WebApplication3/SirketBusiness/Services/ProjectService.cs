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
            // DB'den mevcut projeyi ilişkileriyle çek
            var existingProject = await _context.Projects
                .Include(p => p.UserProjects)
                .Include(p => p.ProjectProducts)
                .FirstOrDefaultAsync(p => p.Id == project.Id);

            if (existingProject == null)
                throw new Exception("Project not found");

            // Eski ilişkileri sil
            _context.UserProjects.RemoveRange(existingProject.UserProjects);
            _context.ProjectProducts.RemoveRange(existingProject.ProjectProducts);

            // Temel bilgileri güncelle
            existingProject.Name = project.Name;

            // Yeni gelen user ilişkilerini ekle
            foreach (var up in project.UserProjects)
            {
                existingProject.UserProjects.Add(new UserProject
                {
                    UserId = up.UserId,
                    ProjectId = existingProject.Id
                });
            }

            // Yeni gelen product ilişkilerini ekle
            foreach (var pp in project.ProjectProducts)
            {
                existingProject.ProjectProducts.Add(new ProjectProduct
                {
                    ProductId = pp.ProductId,
                    ProjectId = existingProject.Id
                });
            }

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
