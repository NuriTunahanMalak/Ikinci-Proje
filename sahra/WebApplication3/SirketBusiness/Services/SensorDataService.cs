using SirketBusiness.Interfaces;
using SirketData;
using SirketEntites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirketBusiness.Services
{
    public class SensorDataService : ISensorDataService
    {
        private readonly SirketDbContext _context;

        public SensorDataService(SirketDbContext context)
        {
            _context = context;
        }

        public async Task<List<SensorData>> GetAllSensorDataAsync()
        {
            return await _context.SensorDatas.ToListAsync();
        }

        public async Task<SensorData> GetSensorDataByIdAsync(int id)
        {
            return await _context.SensorDatas.FindAsync(id);
        }

        public async Task AddSensorDataAsync(SensorData data)
        {
            _context.SensorDatas.Add(data);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSensorDataAsync(SensorData data)
        {
            _context.SensorDatas.Update(data);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSensorDataAsync(int id)
        {
            var data = await _context.SensorDatas.FindAsync(id);
            if (data != null)
            {
                _context.SensorDatas.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
