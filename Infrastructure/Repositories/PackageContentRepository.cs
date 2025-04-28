using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PackageContentRepository : IPackageContentRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public PackageContentRepository(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<int> AddAsync(packagecontent p)
        {
            await _orderDbContext.AddAsync(p);
            await _orderDbContext.SaveChangesAsync();
            int pid = p.id;
            return pid;
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _orderDbContext.packagecontent.FindAsync(id);
            if (p != null)
            {
                _orderDbContext.packagecontent.Remove(p);
                await _orderDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<packagecontent>> GetAllAsync() => await _orderDbContext.packagecontent.ToListAsync();
        public async Task<packagecontent> GetAsync(int id) => await _orderDbContext.packagecontent.FindAsync(id);

        public async Task UpdateAsync(packagecontent p)
        {
            _orderDbContext.packagecontent.Update(p);
            await _orderDbContext.SaveChangesAsync();
        }
    }
}
