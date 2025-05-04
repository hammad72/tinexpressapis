using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICourierStatusMappingRepository
    {
        Task<List<courierstatusmapping>> GetAllAsync();
        Task<courierstatusmapping> GetAsync(int id);
        Task<List<courierstatusmapping>> GetByCIdAsync(int cid);
        Task DeleteAsync(int id);
        Task<bool> AddAsync(List<courierstatusmapping> c);
        Task UpdateAsync(courierstatusmapping c);
    }
}
