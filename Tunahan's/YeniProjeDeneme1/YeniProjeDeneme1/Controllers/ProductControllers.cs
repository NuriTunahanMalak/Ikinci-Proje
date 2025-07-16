using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Services;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using Microsoft.AspNetCore.Authorization;

namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProductControllers: ControllerBase
    {
        private readonly ProductService _productService;
        public ProductControllers(AppDbContex context, ProductService productService)
        {
            _productService = productService;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto productCreateDto)
        {
            await _productService.CreateProduct(productCreateDto);
            return Ok("Product created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateDto productUpdateDto)
        {
            await _productService.UpdateProduct(productUpdateDto);
            return Ok("Product updated successfully.");
        }


    }
}
