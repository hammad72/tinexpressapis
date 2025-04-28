using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CustomerUserProfileRepository : ICustomerUserProfileRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public CustomerUserProfileRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(customeruserprofile cup)
        {
            await _userMgmtDbContext.AddAsync(cup);
            await _userMgmtDbContext.SaveChangesAsync();
            int cpid = cup.id;
            return cpid;
        }

        public async Task DeleteAsync(int id)
        {
            var cup = await _userMgmtDbContext.customeruserprofile.FindAsync(id);
            if (cup != null)
            {
                _userMgmtDbContext.customeruserprofile.Remove(cup);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<customeruserprofile>> GetAllAsync() => await _userMgmtDbContext.customeruserprofile.ToListAsync();
        public async Task<PaginatedList<customeruserprofile>> GetAllAsync(int pageIndex, int pageSize)
        {
            var query = _userMgmtDbContext.customeruserprofile.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<customeruserprofile>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = items
            };
        }
        public async Task<customeruserprofile> GetAsync(int id) => await _userMgmtDbContext.customeruserprofile.FindAsync(id);

        public async Task UpdateAsync(customeruserprofile cup)
        {
            _userMgmtDbContext.customeruserprofile.Update(cup);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}
