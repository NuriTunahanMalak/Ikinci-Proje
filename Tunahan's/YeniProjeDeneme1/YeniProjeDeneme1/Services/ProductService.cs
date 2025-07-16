using FluentValidation;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using Microsoft.EntityFrameworkCore;
using YeniProjeDeneme1.Dtos;

namespace YeniProjeDeneme1.Services
{
    public class ProductService
    {
        private readonly IValidator<ProductCreateDto> _productCreateValidator;
        private readonly IValidator<ProductUpdateDto> _productUpdateValidator;
        private readonly AppDbContex _context;

        public ProductService(IValidator<ProductCreateDto> productCreateValidator,
                              IValidator<ProductUpdateDto> productUpdateValidator,
                              AppDbContex context)
        {
            _productCreateValidator = productCreateValidator;
            _productUpdateValidator = productUpdateValidator;
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await _context.Product
                .Include(p => p.Sensor)
                .Include(p => p.ProjectProducts)
                    .ThenInclude(pp => pp.Project)
                .ToListAsync();
            return products;
        }

        public async Task CreateProduct(ProductCreateDto productCreateDto)
        {
            var result = _productCreateValidator.Validate(productCreateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            if (_context.Product.Any(p => p.Name == productCreateDto.Name))
            {
                throw new Exception("Product already exists with the same name.");
            }

            var product = new Product
            {
                Name = productCreateDto.Name
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var result = _productUpdateValidator.Validate(productUpdateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }

            var product = _context.Product.FirstOrDefault(p => p.Id == productUpdateDto.Id);
            if (product == null)
            {
                throw new Exception("Product not found for update.");
            }

            product.Name = productUpdateDto.Name;
            await _context.SaveChangesAsync();
        }
    }
}
