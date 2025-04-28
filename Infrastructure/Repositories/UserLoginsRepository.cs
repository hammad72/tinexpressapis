using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserLoginsRepository : IUserLoginsRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public UserLoginsRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(userlogins ul)
        {
            ul.status = 1;
            await _userMgmtDbContext.AddAsync(ul);
            await _userMgmtDbContext.SaveChangesAsync();
            int ulid = ul.id;
            return ulid;
        }

        public async Task DeleteAsync(int id)
        {
            var ul = await _userMgmtDbContext.userlogins.FindAsync(id);
            if (ul != null)
            {
                _userMgmtDbContext.userlogins.Remove(ul);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<userlogins>> GetAllAsync() => await _userMgmtDbContext.userlogins.ToListAsync();
        public async Task<userlogins> GetAsync(int id) => await _userMgmtDbContext.userlogins.FindAsync(id);

        public async Task UpdateAsync(userlogins ul)
        {
            _userMgmtDbContext.userlogins.Update(ul);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}
