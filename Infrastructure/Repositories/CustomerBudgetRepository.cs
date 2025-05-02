using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CustomerBudgetRepository : ICustomerBudgetRepository
    {
        private readonly OrderDbContext _DbContext;
        public CustomerBudgetRepository(OrderDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<bool> AddAsync(List<customerbudget> c)
        {
            try
            {
                var oldData = await _DbContext.customerbudget.Where(x => x.customer_id == c[0].customer_id).ToListAsync();
                if (oldData.Count >= 1)
                {
                    _DbContext.customerbudget.RemoveRange(oldData);
                    await _DbContext.SaveChangesAsync();
                }
                await _DbContext.customerbudget.AddRangeAsync(c);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _DbContext.customerbudget.FindAsync(id);
            if (c != null)
            {
                _DbContext.customerbudget.Remove(c);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<List<customerbudget>> GetAllAsync() => await _DbContext.customerbudget.ToListAsync();

        public async Task<customerbudget> GetAsync(int id) => await _DbContext.customerbudget.FindAsync(id);

        public async Task<List<customerbudget>> GetByCIdAsync(int cid)
        {
            return await _DbContext.customerbudget.Where(x => x.customer_id == cid).ToListAsync();
        }

        public async Task UpdateAsync(customerbudget c)
        {
            _DbContext.customerbudget.Update(c);
            await _DbContext.SaveChangesAsync();
        }
    }
}
