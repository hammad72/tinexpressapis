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
    public class OrderStatusesRepository : IOrderStatusesRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public OrderStatusesRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<int> AddAsync(orderstatuses c)
        {
            await _orderDbContext.AddAsync(c);
            await _orderDbContext.SaveChangesAsync();
            int pid = c.id;
            return pid;
        }

        public async Task DeleteAsync(int id)
        {
            var x = await _orderDbContext.orderstatuses.FindAsync(id);
            if (x != null)
            {
                _orderDbContext.orderstatuses.Remove(x);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<orderstatuses>> GetAllAsync()
        {
            var data = await _orderDbContext.orderstatuses.ToListAsync();
            return data;
        }

        public async Task<orderstatuses> GetAsync(int id) => await _orderDbContext.orderstatuses.FindAsync(id);

        public async Task UpdateAsync(orderstatuses c)
        {
            _orderDbContext.orderstatuses.Update(c);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
