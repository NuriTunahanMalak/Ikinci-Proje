using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Services;
using YeniProjeDeneme1.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectControllers : ControllerBase
    {
        private readonly ProjectService projectService;
        public ProjectControllers(ProjectService projectService)
        {
            this.projectService = projectService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var projects = await projectService.GetAllProject();
            return Ok(projects);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] ProjectCreateDto projectCreateDto)
        {
            await projectService.CreateProject(projectCreateDto);
            return Ok("Project created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] ProjectUpdateDto projectUpdateDto)
        {
            await projectService.UpdateProject(projectUpdateDto);
            return Ok("Project updated successfully.");
        }
    }
}   
