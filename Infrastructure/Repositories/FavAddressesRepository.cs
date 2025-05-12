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
    public class FavAddressesRepository : IFavAddressesRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public FavAddressesRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<int> AddAsync(favaddresses fa)
        {
            await _orderDbContext.AddAsync(fa);
            await _orderDbContext.SaveChangesAsync();
            int faid = fa.id;
            return faid;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var fa = await _orderDbContext.favaddresses.FindAsync(id);
            if (fa != null)
            {
                _orderDbContext.favaddresses.Remove(fa);
                await _orderDbContext.SaveChangesAsync();
                return id.ToString();
            }
            else
                return "0";
        }

        public async Task<List<favaddresses>> GetAllAsync()
        {
            var data = await _orderDbContext.favaddresses.ToListAsync();
            return data;
        }

        public async Task<favaddresses> GetAsync(int id) => await _orderDbContext.favaddresses.FindAsync(id); 
        
        public async Task<List<favaddresses>> GetByCIdAsync(int cid)
        {
            var data = await _orderDbContext.favaddresses.Where(x => x.customer_id == cid).ToListAsync();
            return data;
        }

        public async Task UpdateAsync(favaddresses fa)
        {
            _orderDbContext.favaddresses.Update(fa);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
