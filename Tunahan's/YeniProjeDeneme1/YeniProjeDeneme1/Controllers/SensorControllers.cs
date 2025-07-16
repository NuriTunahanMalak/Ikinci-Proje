using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Services;
using YeniProjeDeneme1.Dtos;
using Microsoft.AspNetCore.Authorization;
namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SensorControllers : ControllerBase
    {
        private readonly SensorService _sensorService;
        public SensorControllers(SensorService sensorService)
        {
            this._sensorService = sensorService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var sensors = await _sensorService.GetAllSensor();
            return Ok(sensors);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] SensorCreateDto sensorCreateDto)
        {
            await _sensorService.CreateSensor(sensorCreateDto);
            return Ok("Sensor created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] SensorUpdateDto sensorUpdateDto)
        {
            await _sensorService.UpdateSensor(sensorUpdateDto);
            return Ok("Sensor updated successfully.");


        }
    }
}
