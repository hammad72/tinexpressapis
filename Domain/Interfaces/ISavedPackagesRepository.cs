using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISavedPackagesRepository
    {
        Task<List<savedpackages>> GetAllAsync();
        Task<List<savedpackages>> GetAsyncBySPCode(string spCode);
        Task<List<savedpackages>> GetByCIdAsync(int cid);
        Task<string> DeleteAsync(int id);
        Task<int> AddAsync(List<savedpackages> sp);
        Task UpdateAsync(savedpackages sp);
    }
}
