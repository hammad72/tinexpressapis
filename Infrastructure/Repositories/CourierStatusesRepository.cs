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
    public class CourierStatusesRepository : ICourierStatusesRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public CourierStatusesRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<int> AddAsync(courierstatuses c)
        {
            await _orderDbContext.AddAsync(c);
            await _orderDbContext.SaveChangesAsync();
            int pid = c.id;
            return pid;
        }

        public async Task DeleteAsync(int id)
        {
            var x = await _orderDbContext.courierstatuses.FindAsync(id);
            if (x != null)
            {
                _orderDbContext.courierstatuses.Remove(x);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<courierstatuses>> GetAllAsync()
        {
            var data = await _orderDbContext.courierstatuses.ToListAsync();
            return data;
        }

        public async Task<courierstatuses> GetAsync(int id) => await _orderDbContext.courierstatuses.FindAsync(id);

        public async Task<List<courierstatuses>> GetByCIdAsync(int cid)
        {
            var data = await _orderDbContext.courierstatuses.Where(x => x.courier_id == cid).ToListAsync();
            return data;
        }

        public async Task UpdateAsync(courierstatuses c)
        {
            _orderDbContext.courierstatuses.Update(c);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
