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
    public class CustomerPriorityRepository : ICustomerPriorityRepository
    {
        private readonly OrderDbContext _DbContext;
        public CustomerPriorityRepository(OrderDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<bool> AddAsync(List<customerpriority> c)
        {
            try
            {
                var oldData = await _DbContext.customerpriority.Where(x => x.customer_id == c[0].customer_id).ToListAsync();
                if (oldData.Count >= 1)
                {
                    _DbContext.customerpriority.RemoveRange(oldData);
                    await _DbContext.SaveChangesAsync();
                }
                await _DbContext.customerpriority.AddRangeAsync(c);
                await _DbContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _DbContext.customerpriority.FindAsync(id);
            if (c != null)
            {
                _DbContext.customerpriority.Remove(c);
                await _DbContext.SaveChangesAsync();
            }
        }

        public async Task<List<customerpriority>> GetAllAsync() => await _DbContext.customerpriority.ToListAsync();

        public async Task<customerpriority> GetAsync(int id) => await _DbContext.customerpriority.FindAsync(id);

        public async Task<List<customerpriority>> GetByCIdAsync(int cid)
        {
            return await _DbContext.customerpriority.Where(x => x.customer_id == cid).ToListAsync();
        }

        public async Task UpdateAsync(customerpriority c)
        {
            _DbContext.customerpriority.Update(c);
            await _DbContext.SaveChangesAsync();
        }
    }
}
