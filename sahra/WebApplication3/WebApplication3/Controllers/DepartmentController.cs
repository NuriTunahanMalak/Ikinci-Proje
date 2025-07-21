using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SirketBusiness.Interfaces;
using SirketEntites;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SirketData;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    private readonly IMapper _mapper;
    private readonly SirketDbContext _context;

    // Constructor: Tüm bağımlılıkları burada alıyoruz
    public DepartmentController(IDepartmentService departmentService, IMapper mapper, SirketDbContext context)
    {
        _departmentService = departmentService;
        _mapper = mapper;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllDepartments()
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentUserRole == "Admin")
        {
            var departments = await _departmentService.GetAllAsync();
            var dto = _mapper.Map<List<DepartmentDto>>(departments);
            return Ok(dto);
        }
        else
        {
            // Kullanıcıyı doğrudan User tablosundan bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUserName);
            if (user == null) return Unauthorized();
            var department = await _departmentService.GetByIdAsync(user.DepartmentId);
            if (department == null) return NotFound();
            var dto = _mapper.Map<DepartmentDto>(department);
            return Ok(new List<DepartmentDto> { dto });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var department = await _departmentService.GetByIdAsync(id);
        if (department == null) return NotFound();
        if (currentUserRole != "Admin")
        {
            // Kullanıcıyı doğrudan User tablosundan bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == currentUserName);
            if (user == null || user.DepartmentId != id)
                return Forbid();
        }
        var dto = _mapper.Map<DepartmentDto>(department);
        return Ok(dto);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] DepartmentCreateDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        await _departmentService.AddAsync(entity);
        return Ok();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] DepartmentDto dto)
    {
        if (id != dto.Id) return BadRequest("Id uyuşmuyor");
        var entity = _mapper.Map<Department>(dto);
        await _departmentService.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _departmentService.DeleteAsync(id);
        return NoContent();
    }
}