using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IMarginsRepository
    {
        Task<List<margins>> GetAllAsync();
        Task<margins> GetAsync(int id);
        Task<List<margins>> GetByCIdAsync(int cid);
        Task<string> DeleteAsync(int id);
        Task<bool> AddAsync(List<margins> m);
        Task UpdateAsync(margins m);
    }
}
