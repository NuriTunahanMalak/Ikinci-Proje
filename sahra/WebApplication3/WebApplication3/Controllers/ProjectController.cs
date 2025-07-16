using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirketBusiness.DTOs;
using SirketBusiness.Interfaces;
using SirketEntites;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProjects()
    {
        var currentUserName = User.Identity?.Name; //admin mi user mı diye kontrol usersa yalnızca kendi bilgilerini görür
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentUserRole == "Admin")
        {
            var projects = await _projectService.GetAllProjectsAsync();
            var projectDtos = _mapper.Map<List<ProjectDto>>(projects);
            return Ok(projectDtos);
        }
        else
        {
            // Kullanıcının dahil olduğu projeleri getir
            var projects = await _projectService.GetAllProjectsAsync();
            var userProjects = projects.Where(p => p.UserProjects.Any(up => up.User.Username == currentUserName)).ToList();
            var projectDtos = _mapper.Map<List<ProjectDto>>(userProjects);
            return Ok(projectDtos);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null) return NotFound();
        if (currentUserRole != "Admin" && !project.UserProjects.Any(up => up.User.Username == currentUserName))
            return Forbid();
        var projectDto = _mapper.Map<ProjectDto>(project);
        return Ok(projectDto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto projectCreateDto)
    {
        var project = _mapper.Map<Project>(projectCreateDto);
        await _projectService.AddProjectAsync(project);

        var projectDto = _mapper.Map<ProjectDto>(project);
        return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, projectDto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateProject(int id, [FromBody] ProjectUpdateDto projectUpdateDto)
    {
        if (id <= 0) return BadRequest();
        var project = _mapper.Map<Project>(projectUpdateDto);
        project.Id = id;
        await _projectService.UpdateProjectAsync(project);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        return NoContent();
    }
}
