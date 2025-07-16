using FluentValidation;
using YeniProjeDeneme1.Entities;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Data;
using Microsoft.EntityFrameworkCore;
namespace YeniProjeDeneme1.Services
{
    public class ProjectService
    {
        public readonly IValidator<ProjectCreateDto> _projectCreateValidator;
        public readonly IValidator<ProjectUpdateDto> _projectUpdateValidator;
        public readonly AppDbContex _context;
        public ProjectService(IValidator<ProjectCreateDto> projectCreateValidator,
                                      IValidator<ProjectUpdateDto> projectUpdateValidator,
                                      AppDbContex context)
        {
            _projectCreateValidator = projectCreateValidator;
            _projectUpdateValidator = projectUpdateValidator;
            _context = context;
        }
        public async Task<List<Project>> GetAllProject()
        {
            var project = await _context.Project
                .Include(p => p.ProjectUsers)
                .ToListAsync();
            return project;
        }
        public async Task CreateProject(ProjectCreateDto projectCreateDto)
        {
            var result = _projectCreateValidator.Validate(projectCreateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (_context.Project.Any(p => p.Name == projectCreateDto.Name))
            {
                throw new Exception("Project already exists with the same name.");
            }
            var project = new Project
            {
                Name = projectCreateDto.Name
            };
            _context.Project.Add(project);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProject(ProjectUpdateDto projectUpdateDto)
        {
            var result = _projectUpdateValidator.Validate(projectUpdateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var project = _context.Project.FirstOrDefault(p => p.Id == projectUpdateDto.Id);
            if (project == null)
            {
                throw new Exception("Project not found.");
            }
            project.Name = projectUpdateDto.Name;
            _context.Project.Update(project);
            await _context.SaveChangesAsync();
        }
    }
}
