using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourierStatusMappingRepository : ICourierStatusMappingRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public CourierStatusMappingRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<bool> AddAsync(List<courierstatusmapping> c)
        {
            try
            {
                var x = await _orderDbContext.courierstatusmapping.Where(x=>x.courier_id == c[0].courier_id).ToListAsync();
                if (x.Count>=1 || x != null)
                {
                    _orderDbContext.courierstatusmapping.RemoveRange(x);
                    await _orderDbContext.SaveChangesAsync();
                }


                await _orderDbContext.AddRangeAsync(c);
                await _orderDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var x = await _orderDbContext.courierstatusmapping.FindAsync(id);
            if (x != null)
            {
                _orderDbContext.courierstatusmapping.Remove(x);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<courierstatusmapping>> GetAllAsync()
        {
            var data = await _orderDbContext.courierstatusmapping.ToListAsync();
            return data;
        }

        public async Task<courierstatusmapping> GetAsync(int id) => await _orderDbContext.courierstatusmapping.FindAsync(id);

        public async Task<List<courierstatusmapping>> GetByCIdAsync(int cid)
        {
            var data = await _orderDbContext.courierstatusmapping.Where(x => x.courier_id == cid).ToListAsync();
            return data;
        }

        public async Task UpdateAsync(courierstatusmapping c)
        {
            _orderDbContext.courierstatusmapping.Update(c);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
