using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerUserProfileRepository
    {
        Task<List<customeruserprofile>> GetAllAsync();
        Task<customeruserprofile> GetAsync(int id);
        Task<PaginatedList<customeruserprofile>> GetAllAsync(int pageIndex, int pageSize);
        Task DeleteAsync(int id);
        Task<int> AddAsync(customeruserprofile cup);
        Task UpdateAsync(customeruserprofile cup);
    }
}
