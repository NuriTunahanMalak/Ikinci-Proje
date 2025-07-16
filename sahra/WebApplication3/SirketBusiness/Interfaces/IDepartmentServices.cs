using SirketEntites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SirketBusiness.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int id);
    }
}
