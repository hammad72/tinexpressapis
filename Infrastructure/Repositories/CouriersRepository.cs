using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CouriersRepository : ICouriersRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public CouriersRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(couriers c)
        {
            await _userMgmtDbContext.AddAsync(c);
            await _userMgmtDbContext.SaveChangesAsync();
            int cid = c.id;
            return cid;
        }

        public async Task DeleteAsync(int id)
        {
            var c = await _userMgmtDbContext.couriers.FindAsync(id);
            if (c != null)
            {
                _userMgmtDbContext.couriers.Remove(c);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<couriers>> GetAllAsync() => await _userMgmtDbContext.couriers.ToListAsync();
        public async Task<couriers> GetAsync(int id) => await _userMgmtDbContext.couriers.FindAsync(id);

        public async Task UpdateAsync(couriers c)
        {
            _userMgmtDbContext.couriers.Update(c);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}
