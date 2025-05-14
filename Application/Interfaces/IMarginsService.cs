using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMarginsService
    {
        Task<List<MarginsDto>> GetAllAsync();
        Task<MarginsDto> GetByIdAsync(int id);
        Task<List<MarginsDto>> GetByCIdAsync(int cid);
        Task<bool> AddAsync(List<CreateMarginsDto> cMarginsDto);
        Task<string> DeleteAsync(int id);
        Task UpdateAsync(UpdateMarginsDto uMarginsDto);
    }
}
