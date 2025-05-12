using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
