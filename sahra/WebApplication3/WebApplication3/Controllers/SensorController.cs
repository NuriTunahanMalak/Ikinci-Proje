using Microsoft.AspNetCore.Mvc;
using SirketBusiness.Interfaces;
using SirketBusiness.DTOs;
using AutoMapper;
using SirketEntites;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/[controller]")]
public class SensorController : ControllerBase
{
    private readonly ISensorService _sensorService;
    private readonly IMapper _mapper;

    public SensorController(ISensorService sensorService, IMapper mapper)
    {
        _sensorService = sensorService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var sensors = await _sensorService.GetAllSensorsAsync();

        if (currentUserRole == "Admin")
        {
            // Admin ise tüm sensörleri döndür
            return Ok(_mapper.Map<List<SensorDto>>(sensors));
        }
        else
        { 
            // User ise sadece kendi projelerine ait sensörleri döndür
            var userSensors = sensors.Where(s =>
                s.Product != null &&
                s.Product.ProjectProducts != null &&
                s.Product.ProjectProducts.Any(pp =>
                    pp.Project != null &&
                    pp.Project.UserProjects != null &&
                    pp.Project.UserProjects.Any(up =>
                        up.User != null && up.User.Username == currentUserName
                    )
                ) 
            ).ToList();
            return Ok(_mapper.Map<List<SensorDto>>(userSensors));
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var currentUserName = User.Identity?.Name;
        var currentUserRole = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Role)?.Value;
        var sensor = await _sensorService.GetSensorByIdAsync(id);
        if (sensor == null) return NotFound();

        if (currentUserRole != "Admin")
        { 
           if (sensor.Product == null ||
                sensor.Product.ProjectProducts == null ||
                !sensor.Product.ProjectProducts.Any(pp =>
                    pp.Project != null &&
                    pp.Project.UserProjects != null &&
                    pp.Project.UserProjects.Any(up =>
                        up.User != null && up.User.Username == currentUserName
                   
                )))
            {
                return Forbid();
            }
        }

        return Ok(_mapper.Map<SensorDto>(sensor));
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] SensorCreateDto dto)
    {
        var sensor = _mapper.Map<Sensor>(dto);
        await _sensorService.AddSensorAsync(sensor);
        return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, _mapper.Map<SensorDto>(sensor));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sensorService.DeleteSensorAsync(id);
        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] SensorCreateDto dto)
    {
        var sensor = _mapper.Map<Sensor>(dto);
        sensor.Id = id;
        await _sensorService.UpdateSensorAsync(sensor);
        return NoContent();
    }
}