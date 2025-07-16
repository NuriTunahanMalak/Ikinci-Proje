using Microsoft.EntityFrameworkCore;
using SirketBusiness.Interfaces;
using SirketData;
using SirketEntites;

namespace SirketBusiness.Services
{
    public class ProductService : IProductService
    {
        private readonly SirketDbContext _context;

        public ProductService(SirketDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProjectProducts) // projelerle ilişkili ürünler
                .Include(p => p.Sensors)         // sensörlerle ilişkili ürünler
                .ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProjectProducts)
                .Include(p => p.Sensors)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
