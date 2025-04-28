using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICouriersRepository
    {
        Task<List<couriers>> GetAllAsync();
        Task<couriers> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(couriers c);
        Task UpdateAsync(couriers c);
    }
}
