using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerPriorityRepository
    {
        Task<List<customerpriority>> GetAllAsync();
        Task<customerpriority> GetAsync(int id);
        Task<List<customerpriority>> GetByCIdAsync(int cid);
        Task DeleteAsync(int id);
        Task<bool> AddAsync(List<customerpriority> c);
        Task UpdateAsync(customerpriority c);
    }
}
