using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Services;
using YeniProjeDeneme1.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectProductControllers:ControllerBase
    {
        private readonly ProjectProductService _projectProductService;
        public ProjectProductControllers(ProjectProductService projectProductService)
        {
            _projectProductService = projectProductService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var projectProducts = await _projectProductService.GetAll();
            return Ok(projectProducts);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProjectProductCreateDto projectProductCreateDto)
        {
            await _projectProductService.CreateProjectProduct(projectProductCreateDto);
            return Ok("Project product created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProjectProductUpdateDto projectProductUpdateDto)
        {
            await _projectProductService.UpdateProjectProduct(projectProductUpdateDto);
            return Ok("Project product updated successfully.");
        }

    }
}
