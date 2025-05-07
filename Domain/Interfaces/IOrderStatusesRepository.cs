using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderStatusesRepository
    {
        Task<List<orderstatuses>> GetAllAsync();
        Task<orderstatuses> GetAsync(int id);
        Task DeleteAsync(int id);
        Task<int> AddAsync(orderstatuses c);
        Task UpdateAsync(orderstatuses c);
    }
}
