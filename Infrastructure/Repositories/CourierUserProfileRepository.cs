using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class CourierUserProfileRepository : ICourierUserProfileRepository
    {
        private readonly UserMgmtDbContext _userMgmtDbContext;
        public CourierUserProfileRepository(UserMgmtDbContext userMgmtDbContext)
        {
            _userMgmtDbContext = userMgmtDbContext;
        }

        public async Task<int> AddAsync(courieruserprofile cup)
        {
            await _userMgmtDbContext.AddAsync(cup);
            await _userMgmtDbContext.SaveChangesAsync();
            int cpid = cup.id;
            return cpid;
        }

        public async Task DeleteAsync(int id)
        {
            var cup = await _userMgmtDbContext.courieruserprofile.FindAsync(id);
            if (cup != null)
            {
                _userMgmtDbContext.courieruserprofile.Remove(cup);
                await _userMgmtDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<courieruserprofile>> GetAllAsync() => await _userMgmtDbContext.courieruserprofile.ToListAsync();
        public async Task<courieruserprofile> GetAsync(int id) => await _userMgmtDbContext.courieruserprofile.FindAsync(id);

        public async Task UpdateAsync(courieruserprofile cup)
        {
            _userMgmtDbContext.courieruserprofile.Update(cup);
            await _userMgmtDbContext.SaveChangesAsync();
        }
    }
}