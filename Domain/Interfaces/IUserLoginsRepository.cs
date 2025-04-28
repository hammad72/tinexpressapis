using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserLoginsRepository
    {
        Task<List<userlogins>> GetAllAsync();
        Task<userlogins> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(userlogins ul);
        Task UpdateAsync(userlogins ul);
    }
}
