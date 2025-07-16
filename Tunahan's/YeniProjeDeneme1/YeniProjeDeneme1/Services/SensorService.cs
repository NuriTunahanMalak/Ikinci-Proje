using FluentValidation;
using YeniProjeDeneme1.Dtos;
using YeniProjeDeneme1.Data;
using YeniProjeDeneme1.Entities;
using Microsoft.EntityFrameworkCore;
namespace YeniProjeDeneme1.Services
{
    public class SensorService
    {
        public readonly IValidator<SensorCreateDto> _sensorCreateValidator;
        public readonly IValidator<SensorUpdateDto> _sensorUpdateValidator;
        public readonly AppDbContex _context;
        public SensorService(IValidator<SensorCreateDto> sensorCreateValidator,
                             IValidator<SensorUpdateDto> sensorUpdateValidator,
                             AppDbContex context)
        {
            _sensorCreateValidator = sensorCreateValidator;
            _sensorUpdateValidator = sensorUpdateValidator;
            _context = context;
        }
        public async Task<List<Sensor>> GetAllSensor()
        {
            var sensors= _context.Sensor
                .Include(s => s.Sensor_Datas)
                .ToListAsync();
            return await sensors;
        }
        public async Task CreateSensor(SensorCreateDto sensordto)
        {
            var result=_sensorCreateValidator.Validate(sensordto);
            if(!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var sensors = _context.Sensor.ToList();
            if (sensors.Any(s => s.Name == sensordto.Name && s.ProductId == sensordto.ProductId))
            {
                throw new Exception("Sensor already exists with the same name and product ID.");
            }
            var sensor = new Sensor
            {
                Id = sensordto.Id,
                Name = sensordto.Name,
                ProductId = sensordto.ProductId
            };
            _context.Sensor.Add(sensor);
            await _context.SaveChangesAsync();
            
        }
        public async Task UpdateSensor(SensorUpdateDto sensordto)
        {
            var result = _sensorUpdateValidator.Validate(sensordto);
            if (!result.IsValid)
            {
                throw new ValidationException(result.Errors);
            }
            var sensors = _context.Sensor.ToList();
            if (!sensors.Any(s => s.Id == sensordto.Id))
            {
                throw new Exception("You can do just update");
            }
            var sensora = sensors.FirstOrDefault(s => s.Id == sensordto.Id);
            if (sensora == null)
            {
                throw new Exception("Sensor not found.");
            }
            sensora.Name = sensordto.Name;
            sensora.ProductId = sensordto.ProductId;
            _context.Sensor.Update(sensora);
            await _context.SaveChangesAsync();

        }
    }
}
