using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFavAddressesRepository
    {
        Task<List<favaddresses>> GetAllAsync();
        Task<favaddresses> GetAsync(int id);
        Task<List<favaddresses>> GetByCIdAsync(int cid);
        Task<string> DeleteAsync(int id);
        Task<int> AddAsync(favaddresses fa);
        Task UpdateAsync(favaddresses fa);
    }
}
