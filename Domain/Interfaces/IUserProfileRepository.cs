using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<List<userprofile>> GetAllAsync(); 
        Task<PaginatedList<userprofile>> GetAllAsync(int pageIndex, int pageSize);

        Task<userprofile> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(userprofile up);
        Task UpdateAsync(userprofile up);
    }
}
