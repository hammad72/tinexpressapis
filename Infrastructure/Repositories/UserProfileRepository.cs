using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public UserProfileRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(userprofile up)
        {
            await _userMgmtDbContext.AddAsync(up);
            await _userMgmtDbContext.SaveChangesAsync();
            int ulid = up.id;
            return ulid;
        }

        public async Task DeleteAsync(int id)
        {
            var up = await _userMgmtDbContext.userprofile.FindAsync(id);
            if (up != null)
            {
                _userMgmtDbContext.userprofile.Remove(up);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<userprofile>> GetAllAsync() => await _userMgmtDbContext.userprofile.ToListAsync();
        public async Task<PaginatedList<userprofile>> GetAllAsync(int pageIndex, int pageSize)
        {
            var query = _userMgmtDbContext.userprofile.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PaginatedList<userprofile>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = items
            };
        }

        public async Task<userprofile> GetAsync(int id) => await _userMgmtDbContext.userprofile.FindAsync(id);

        public async Task UpdateAsync(userprofile up)
        {
            _userMgmtDbContext.userprofile.Update(up);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}
