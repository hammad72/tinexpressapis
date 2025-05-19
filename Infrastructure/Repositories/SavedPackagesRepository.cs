using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SavedPackagesRepository : ISavedPackagesRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public SavedPackagesRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }
        public async Task<int> AddAsync(List<savedpackages> sp)
        {
            await _orderDbContext.AddRangeAsync(sp);
            await _orderDbContext.SaveChangesAsync();
            //int spid = sp.id;
            return 1;
        }

        public async Task<string> DeleteAsync(int id)
        {
            var sp = await _orderDbContext.savedpackages.FindAsync(id);
            if (sp != null)
            {
                _orderDbContext.savedpackages.Remove(sp);
                await _orderDbContext.SaveChangesAsync();
                return id.ToString();
            }
            else
                return "0";
        }

        public async Task<List<savedpackages>> GetAllAsync()
        {
            var data = await _orderDbContext.savedpackages.ToListAsync();
            return data;
        }

        public async Task<List<savedpackages>> GetAsyncBySPCode(string spCode)
        {
            var data = await _orderDbContext.savedpackages.Where(x => x.sp_code == spCode).ToListAsync();
            return data;
        }

        public async Task<List<savedpackages>> GetByCIdAsync(int cid)
        {
            var data = await _orderDbContext.savedpackages.Where(x => x.customer_id == cid).ToListAsync();
            return data;
        }

        public async Task UpdateAsync(savedpackages sp)
        {
            _orderDbContext.savedpackages.Update(sp);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
