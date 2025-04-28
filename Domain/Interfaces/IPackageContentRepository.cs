using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPackageContentRepository
    {
        Task<List<packagecontent>> GetAllAsync();
        Task<packagecontent> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(packagecontent pc);
        Task UpdateAsync(packagecontent pc);
    }
}
