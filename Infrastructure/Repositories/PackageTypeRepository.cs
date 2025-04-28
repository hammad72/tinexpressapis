using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PackageTypeRepository : IPackageTypeRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public PackageTypeRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<int> AddAsync(packagetype p)
        {
            await _orderDbContext.AddAsync(p);
            await _orderDbContext.SaveChangesAsync();
            int pid = p.id;
            return pid;
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _orderDbContext.packagetype.FindAsync(id);
            if (p != null)
            {
                _orderDbContext.packagetype.Remove(p);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        //public async Task<List<packagetype>> GetAllAsync() => await _orderDbContext.packagetype.ToListAsync();
        public async Task<List<packagetype>> GetAllAsync()
        {
            //List<packagetype> lpt = new List<packagetype>();
            //    packagetype pt = new packagetype();
            //    pt.id = 1;
            //    pt.title = "test";
            //    lpt.Add(pt);
            //    return lpt;
            var data = await _orderDbContext.packagetype.ToListAsync();
            return data;
        }
        public async Task<packagetype> GetAsync(int id) => await _orderDbContext.packagetype.FindAsync(id);

        public async Task UpdateAsync(packagetype p)
        {
            _orderDbContext.packagetype.Update(p);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
