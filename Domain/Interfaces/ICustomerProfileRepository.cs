using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ICustomerProfileRepository
    {
        Task<List<customerprofile>> GetAllAsync();
        Task<customerprofile> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(customerprofile cp);
        Task UpdateAsync(customerprofile cp);
    }
}
