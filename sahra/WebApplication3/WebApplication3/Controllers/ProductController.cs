using Microsoft.AspNetCore.Mvc;
using SirketBusiness.Interfaces;
using SirketBusiness.DTOs;
using AutoMapper;
using SirketEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SirketData;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;
    private readonly SirketDbContext _context;

    public ProductController(IProductService productService, IMapper mapper, SirketDbContext context)
    {
        _productService = productService;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentUserRole == "Admin")
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }
        else
        {
            // Kullanıcıyı bul
            var user = await _context.Users
                .Include(u => u.UserProjects)
                .FirstOrDefaultAsync(u => u.Username == currentUserName);

            if (user == null) return Unauthorized();

            // Kullanıcının dahil olduğu projelerin ID'leri
            var userProjectIds = user.UserProjects.Select(up => up.ProjectId).ToList();

            // Tüm ürünleri çek
            var products = await _productService.GetAllProductsAsync();

            // Sadece kullanıcının projelerine ait ürünler
            var userProducts = products.Where(p =>
                p.ProjectProducts?.Any(pp => userProjectIds.Contains(pp.ProjectId)) == true
            ).ToList();

            var productDtos = _mapper.Map<List<ProductDto>>(userProducts);
            return Ok(productDtos);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();

        if (currentUserRole != "Admin")
        {
            var user = await _context.Users
                .Include(u => u.UserProjects)
                .FirstOrDefaultAsync(u => u.Username == currentUserName);

            if (user == null) return Unauthorized();

            var userProjectIds = user.UserProjects.Select(up => up.ProjectId).ToList();

            if (product.ProjectProducts?.Any(pp => userProjectIds.Contains(pp.ProjectId)) != true)
                return Forbid();
        }

        var productDto = _mapper.Map<ProductDto>(product);
        return Ok(productDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
    {
        var product = _mapper.Map<Product>(productCreateDto);
        await _productService.AddProductAsync(product);

        var productDto = _mapper.Map<ProductDto>(product);
        return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, productDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        var product = _mapper.Map<Product>(productUpdateDto);
        product.Id = id;
        await _productService.UpdateProductAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProductAsync(id);
        return NoContent();
    }
}