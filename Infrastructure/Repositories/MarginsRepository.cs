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
    public class MarginsRepository : IMarginsRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public MarginsRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<bool> AddAsync(List<margins> m)
        {
            try
            {
                var _margins = await _orderDbContext.margins.ToListAsync();//.Where(x => x.courier_id == m[0].courier_id).ToListAsync();
                if (_margins.Count >= 1)
                {
                    _orderDbContext.margins.RemoveRange(_margins);
                    await _orderDbContext.SaveChangesAsync();
                }
                await _orderDbContext.margins.AddRangeAsync(m);
                await _orderDbContext.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<string> DeleteAsync(int id)
        {
            var m = await _orderDbContext.margins.FindAsync(id);
            if (m != null)
            {
                _orderDbContext.margins.Remove(m);
                await _orderDbContext.SaveChangesAsync();
                return id.ToString();
            }
            else
                return "0";
        }

        public async Task<List<margins>> GetAllAsync()
        {
            var data = await _orderDbContext.margins.ToListAsync();
            return data;
        }

        public async Task<margins> GetAsync(int id) => await _orderDbContext.margins.FindAsync(id);

        public async Task<List<margins>> GetByCIdAsync(int cid)
        {
            var data = await _orderDbContext.margins.Where(x => x.courier_id == cid).ToListAsync();
            return data;
        }

        public async Task UpdateAsync(margins m)
        {
            _orderDbContext.margins.Update(m);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
