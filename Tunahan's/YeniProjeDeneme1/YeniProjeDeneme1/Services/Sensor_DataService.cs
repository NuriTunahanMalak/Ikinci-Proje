using FluentValidation;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using Microsoft.EntityFrameworkCore;
namespace YeniProjeDeneme1.Services
{
    public class Sensor_DataService
    {
        private readonly IValidator<SensorDataCreateDto> _sensorDataCreateValidator;
        private readonly IValidator<SensorDataUpdateDto> _sensorDataUpdateValidator;
        private readonly AppDbContex _context;
        public Sensor_DataService(IValidator<SensorDataCreateDto> sensorDataCreateValidator,
                           IValidator<SensorDataUpdateDto> sensorDataUpdateValidator,
                           AppDbContex context)
        {
            _sensorDataCreateValidator = sensorDataCreateValidator;
            _sensorDataUpdateValidator = sensorDataUpdateValidator;
            _context = context;
        }
        public async Task<List<Sensor_Data>> GetShowAll()
        {
            var sensorDatas = _context.Sensor_Data
                .ToListAsync();
            return await sensorDatas;
        }

        public async Task CreateSensorDataAsync(SensorDataCreateDto sensorDataCreateDto)
        {
            var result=_sensorDataCreateValidator.Validate(sensorDataCreateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var sensor = _context.Sensor_Data.ToList();
            if (sensor.Any(s => s.SensorId == sensorDataCreateDto.SensorId))
            {
                throw new Exception("Sensor data already exists for this sensor.");
            }
            var sensorData = new Sensor_Data
            {
                SensorId = sensorDataCreateDto.SensorId,
                data = sensorDataCreateDto.data
            };
            _context.Sensor_Data.Add(sensorData);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateSensorDataAsync(SensorDataUpdateDto sensorDataUpdateDto)
        {
            var result = _sensorDataUpdateValidator.Validate(sensorDataUpdateDto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var sensorDatas=_context.Sensor_Data.ToList();
            if (!sensorDatas.Any(s=>s.SensorId == sensorDataUpdateDto.SensorId))
            {
                throw new Exception("There arenot any sensorıd like that.");

            }
            var sensorData=  sensorDatas.FirstOrDefault(s=> s.Id == sensorDataUpdateDto.Id);
            if (sensorData==null)
            {
                throw new Exception("This is null.");

            }
            sensorData.SensorId = sensorDataUpdateDto.SensorId;
            sensorData.data = sensorDataUpdateDto.data;
            _context.Sensor_Data.Update(sensorData);
            await _context.SaveChangesAsync();

        }

    }
}
