using FluentValidation;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using Microsoft.EntityFrameworkCore;
namespace YeniProjeDeneme1.Services
{
    public class ProjectUserService
    {
        public readonly IValidator<ProjectUserCreateDto> _productUserCreateValidator;
        public readonly IValidator<ProjectUserUpdateDto> _productUserUpdateValidator;
        public readonly AppDbContex _context;
        public ProjectUserService(IValidator<ProjectUserCreateDto> productUserCreateValidator, IValidator<ProjectUserUpdateDto> productUserUpdateValidator, AppDbContex context)
        {
            _productUserCreateValidator = productUserCreateValidator;
            _productUserUpdateValidator = productUserUpdateValidator;
            _context = context;
        }
        public Task<List<ProjectUser>> GetAllProjectUser()
        {
            var projectUsers = _context.ProjectUser
                .Include(pu => pu.User)
                .Include(pu => pu.Project)
                .ToListAsync();
            return projectUsers;
        }
        public Task CreateProjectUser(ProjectUserCreateDto projectUserCreateDto)
        {
            var result= _productUserCreateValidator.Validate(projectUserCreateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (_context.ProjectUser.Any(pu => pu.UserId == projectUserCreateDto.UserId && pu.ProjectId == projectUserCreateDto.ProjectId))
            {
                throw new Exception("Project user already exists with the same user and project.");
            }
            var projectUser = new ProjectUser
            {
                UserId = projectUserCreateDto.UserId,
                ProjectId = projectUserCreateDto.ProjectId
            };
            _context.ProjectUser.Add(projectUser);
            return _context.SaveChangesAsync();
        }
        public async Task UpdateProjectUser(ProjectUserUpdateDto projectUserUpdateDto)
        {
            var result = _productUserUpdateValidator.Validate(projectUserUpdateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var projectUser = await _context.ProjectUser.FirstOrDefaultAsync(pu => pu.Id == projectUserUpdateDto.Id);
            if (projectUser == null)
            {
                throw new Exception("Project user not found.");
            }
            projectUser.UserId = projectUserUpdateDto.UserId;
            projectUser.ProjectId = projectUserUpdateDto.ProjectId;
            _context.ProjectUser.Update(projectUser);
            await _context.SaveChangesAsync();
        }
    }
}
