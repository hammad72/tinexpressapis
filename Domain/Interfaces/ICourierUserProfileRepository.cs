using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICourierUserProfileRepository
    {
        Task<List<courieruserprofile>> GetAllAsync();
        Task<courieruserprofile> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(courieruserprofile cup);
        Task UpdateAsync(courieruserprofile cup);
    }
}
