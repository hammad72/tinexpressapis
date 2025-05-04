using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerBudgetRepository
    {
        Task<List<customerbudget>> GetAllAsync();
        Task<customerbudget> GetAsync(int id);
        Task<List<customerbudget>> GetByCIdAsync(int cid);
        Task DeleteAsync(int id);
        Task<bool> AddAsync(List<customerbudget> c);
        Task UpdateAsync(customerbudget c);
    }
}
