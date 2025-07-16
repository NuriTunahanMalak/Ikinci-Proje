using SirketEntites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirketBusiness.Interfaces
{
    public interface ISensorDataService
    {
        Task<List<SensorData>> GetAllSensorDataAsync();
        Task<SensorData> GetSensorDataByIdAsync(int id);
        Task AddSensorDataAsync(SensorData data);
        Task UpdateSensorDataAsync(SensorData data);
        Task DeleteSensorDataAsync(int id);
    }
}
