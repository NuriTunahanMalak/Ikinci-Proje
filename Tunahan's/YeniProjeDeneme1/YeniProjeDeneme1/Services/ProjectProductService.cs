using FluentValidation;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Validator;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using Microsoft.EntityFrameworkCore;
namespace YeniProjeDeneme1.Services
{
    public class ProjectProductService
    {
        public readonly IValidator<ProjectProductCreateDto> _projectProductCreateValidator;
        public readonly IValidator<ProjectProductUpdateDto> _projectProductUpdateValidator;
        public readonly AppDbContex _context;
        public ProjectProductService(IValidator<ProjectProductCreateDto> projectProductCreateValidator,
                                     IValidator<ProjectProductUpdateDto> projectProductUpdateValidator,
                                     AppDbContex context)
        {
            _projectProductCreateValidator = projectProductCreateValidator;
            _projectProductUpdateValidator = projectProductUpdateValidator;
            _context = context;
        }
        public async Task<List<ProjectProduct>> GetAll()
        {
            var projectProducts = await _context.ProjectProduct
                .ToListAsync();
            return projectProducts;
        }
        public async Task CreateProjectProduct(ProjectProductCreateDto projectProductCreateDto)
        {
            var result=_projectProductCreateValidator.Validate(projectProductCreateDto);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            if (_context.ProjectProduct.Any(p => p.ProjectId == projectProductCreateDto.ProjectId)&& _context.ProjectProduct.Any(p => p.ProductId == projectProductCreateDto.ProductId))
            {
                throw new Exception("Project product already exists with the same name.");
            }
            var projectProduct = new ProjectProduct
            {
                ProjectId = projectProductCreateDto.ProjectId,
                ProductId = projectProductCreateDto.ProductId
            };
            _context.ProjectProduct.Add(projectProduct);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProjectProduct(ProjectProductUpdateDto projectProductUpdateDto)
        {
            var result = _projectProductUpdateValidator.Validate(projectProductUpdateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var projectProduct=_context.ProjectProduct.FirstOrDefault(p => p.Id == projectProductUpdateDto.Id);
            if (projectProduct == null)
            {
                throw new Exception("Project product not found.");
            }
            projectProduct.ProjectId = projectProductUpdateDto.ProjectId;
            projectProduct.ProductId = projectProductUpdateDto.ProductId;
            _context.ProjectProduct.Update(projectProduct);
            await _context.SaveChangesAsync();
        }
    }
}
