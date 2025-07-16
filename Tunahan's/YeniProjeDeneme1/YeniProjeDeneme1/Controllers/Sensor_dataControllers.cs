using YeniProjeDeneme1.Services;
using Microsoft.AspNetCore.Mvc;
using YeniProjeDeneme1.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace YeniProjeDeneme1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class Sensor_dataControllers:ControllerBase
    {
        private readonly Sensor_DataService _sensorDataService;
        public Sensor_dataControllers(Sensor_DataService sensorDataService)
        {
            _sensorDataService = sensorDataService;
        }
        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetAll()
        {
            var datas = await _sensorDataService.GetShowAll();
            return Ok(datas);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Adding(SensorDataCreateDto _sensorDataCreateDto)
        {
            await _sensorDataService.CreateSensorDataAsync(_sensorDataCreateDto);
            return Ok("Sensor data created successfully.");
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(SensorDataUpdateDto _sensorDataUpdateDto)
        {
            await _sensorDataService.UpdateSensorDataAsync(_sensorDataUpdateDto);
            return Ok("Sensor data updated successfully.");
        }

    }
}
