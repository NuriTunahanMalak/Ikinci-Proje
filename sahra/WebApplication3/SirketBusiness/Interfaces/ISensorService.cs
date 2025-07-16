using Microsoft.EntityFrameworkCore;
using SirketEntites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirketBusiness.Interfaces
{
    public interface ISensorService
    {
        Task<List<Sensor>> GetAllSensorsAsync();
        Task<Sensor> GetSensorByIdAsync(int id);
  
        Task DeleteSensorAsync (int id);
        Task UpdateSensorAsync (Sensor sensor);
        Task AddSensorAsync(Sensor sensor);

 
    }
}
