using Microsoft.AspNetCore.Mvc;
using SirketBusiness.Interfaces;
using SirketBusiness.DTOs;
using AutoMapper;
using SirketEntites;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var currentUserName = User.Identity?.Name; //Sisteme şu anda giriş yapmış kullanıcının adı
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentUserRole == "Admin")
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }
        else
        {
            // Kullanıcının dahil olduğu projelerdeki ürünleri getir
            var products = await _productService.GetAllProductsAsync();
            var userProducts = products.Where(p => p.ProjectProducts.Any(pp => pp.Project.UserProjects.Any(up => up.User.Username == currentUserName))).ToList();
            var productDtos = _mapper.Map<List<ProductDto>>(userProducts);
            return Ok(productDtos);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;//kullanıcının rolunu alıyoruz
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null) return NotFound();
        if (currentUserRole != "Admin" && !product.ProjectProducts.Any(pp => pp.Project.UserProjects.Any(up => up.User.Username == currentUserName)))
            return Forbid();  //admin mi veya projeye dahil bir kullanıcı mı 
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
