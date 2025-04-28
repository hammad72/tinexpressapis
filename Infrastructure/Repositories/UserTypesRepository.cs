using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserTypesRepository : IUserTypesRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public UserTypesRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(usertypes u)
        {
            await _userMgmtDbContext.AddAsync(u);
            await _userMgmtDbContext.SaveChangesAsync();
            int uid = u.id;
            return uid;
        }

        public async Task DeleteAsync(int id)
        {
            var u = await _userMgmtDbContext.usertypes.FindAsync(id);
            if (u != null)
            {
                _userMgmtDbContext.usertypes.Remove(u);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        //public async Task<List<packagetype>> GetAllAsync() => await _orderDbContext.packagetype.ToListAsync();
        public async Task<List<usertypes>> GetAllAsync()
        {
            var data = await _userMgmtDbContext.usertypes.ToListAsync();
            return data;
        }
        public async Task<List<usertypes>> GetUserTypesAllByPlatform(int pid)
        {
            var data = await _userMgmtDbContext.usertypes.Where(u => u.plateform_id == pid).ToListAsync();
            return data;
        }
        public async Task<usertypes> GetAsync(int id) => await _userMgmtDbContext.usertypes.FindAsync(id);

        public async Task UpdateAsync(usertypes u)
        {
            _userMgmtDbContext.usertypes.Update(u);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}
