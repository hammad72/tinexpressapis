using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserTypesRepository
    {
        Task<List<usertypes>> GetAllAsync();
        Task<List<usertypes>> GetUserTypesAllByPlatform(int pid);
        Task<usertypes> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(usertypes ut);
        Task UpdateAsync(usertypes ut);
    }
}
