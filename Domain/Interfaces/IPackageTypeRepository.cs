using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPackageTypeRepository
    {
        Task<List<packagetype>> GetAllAsync();
        Task<packagetype> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(packagetype pt);
        Task UpdateAsync(packagetype pt);
    }
}
