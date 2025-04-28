using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerProfileRepository : ICustomerProfileRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public CustomerProfileRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(customerprofile cp)
        {
            await _userMgmtDbContext.AddAsync(cp);
            await _userMgmtDbContext.SaveChangesAsync();
            int cpid = cp.id;
            return cpid;
        }

        public async Task DeleteAsync(int id)
        {
            var cp = await _userMgmtDbContext.customerprofile.FindAsync(id);
            if (cp != null)
            {
                _userMgmtDbContext.customerprofile.Remove(cp);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<customerprofile>> GetAllAsync() => await _userMgmtDbContext.customerprofile.ToListAsync();
        public async Task<customerprofile> GetAsync(int id) => await _userMgmtDbContext.customerprofile.FindAsync(id);

        public async Task UpdateAsync(customerprofile cp)
        {
            _userMgmtDbContext.customerprofile.Update(cp);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}
