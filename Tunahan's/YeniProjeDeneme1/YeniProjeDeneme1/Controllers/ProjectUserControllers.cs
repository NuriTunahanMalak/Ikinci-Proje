using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Services;
using YeniProjeDeneme1.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectUserControllers:ControllerBase
    {
        private readonly ProjectUserService _projectUserService;
        public ProjectUserControllers(ProjectUserService projectUserService)
        {
            _projectUserService = projectUserService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var projectUsers = await _projectUserService.GetAllProjectUser();
            return Ok(projectUsers);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProjectUserCreateDto projectUserCreateDto)
        {
            await _projectUserService.CreateProjectUser(projectUserCreateDto);
            return Ok("Project user created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProjectUserUpdateDto projectUserUpdateDto)
        {
            await _projectUserService.UpdateProjectUser(projectUserUpdateDto);
            return Ok("Project user updated successfully.");
        }

    }
}
