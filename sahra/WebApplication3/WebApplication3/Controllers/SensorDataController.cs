using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SirketBusiness.Interfaces;
using SirketBusiness.DTOs;
using AutoMapper;
using SirketEntites;

[ApiController]
[Route("api/[controller]")]
public class SensorDataController : ControllerBase
{
    private readonly ISensorDataService _sensorDataService;
    private readonly IMapper _mapper;

    public SensorDataController(ISensorDataService sensorDataService, IMapper mapper)
    {
        _sensorDataService = sensorDataService;
        _mapper = mapper;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll()
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        if (currentUserRole == "Admin")
        {
            var data = await _sensorDataService.GetAllSensorDataAsync();
            return Ok(_mapper.Map<List<SensorDataDto>>(data));
        }
        else
        {
            var data = await _sensorDataService.GetAllSensorDataAsync();
            var userData = data.Where(d => d.Sensor.Product.ProjectProducts.Any(pp => pp.Project.UserProjects.Any(up => up.User.Username == currentUserName))).ToList();
            return Ok(_mapper.Map<List<SensorDataDto>>(userData));
        }
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var data = await _sensorDataService.GetSensorDataByIdAsync(id);
        if (data == null) return NotFound();
        if (currentUserRole != "Admin" && !data.Sensor.Product.ProjectProducts.Any(pp => pp.Project.UserProjects.Any(up => up.User.Username == currentUserName)))
            return Forbid();
        return Ok(_mapper.Map<SensorDataDto>(data));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] SensorDataCreateDto dto)
    {
        var entity = _mapper.Map<SensorData>(dto);
        await _sensorDataService.AddSensorDataAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.Map<SensorDataDto>(entity));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] SensorDataCreateDto dto)
    {
        var entity = _mapper.Map<SensorData>(dto);
        entity.Id = id;
        await _sensorDataService.UpdateSensorDataAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sensorDataService.DeleteSensorDataAsync(id);
        return NoContent();
    }
}
