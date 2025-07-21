using SirketBusiness.Interfaces;
using SirketData;
using SirketEntites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirketBusiness.Services
{
    public class SensorService : ISensorService
    {
        private readonly SirketDbContext _context;

        public SensorService(SirketDbContext context)
        {
            _context = context;
        }

        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            return await _context.Sensors
                .Include(s => s.Product)
                    .ThenInclude(p => p.ProjectProducts)
                        .ThenInclude(pp => pp.Project)
                            .ThenInclude(pr => pr.UserProjects)
                                .ThenInclude(up => up.User)
                .ToListAsync();
        }

        public async Task<Sensor> GetSensorByIdAsync(int id)
        {
            return await _context.Sensors
                .Include(s => s.Product)
                    .ThenInclude(p => p.ProjectProducts)
                        .ThenInclude(pp => pp.Project)
                            .ThenInclude(pr => pr.UserProjects)
                                .ThenInclude(up => up.User)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddSensorAsync(Sensor sensor)
        {
            _context.Sensors.Add(sensor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSensorAsync(int id)
        {
            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor != null)
            {
                _context.Sensors.Remove(sensor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateSensorAsync(Sensor sensor)
        {
            _context.Sensors.Update(sensor);
            await _context.SaveChangesAsync();
        }
    }
}
