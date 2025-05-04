using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICourierStatusesRepository
    {
        Task<List<courierstatuses>> GetAllAsync();
        Task<courierstatuses> GetAsync(int id);
        Task<List<courierstatuses>> GetByCIdAsync(int cid);
        Task DeleteAsync(int id);
        Task<int> AddAsync(courierstatuses c);
        Task UpdateAsync(courierstatuses c);
    }
}
