using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISavedPackagesService
    {
        Task<List<SavedPackagesDto>> GetAllAsync();
        Task<List<SavedPackagesDto>> GetAsyncBySPCode(string spCode);
        Task<List<SavedPackagesDto>> GetByCIdAsync(int cid);
        Task<int> AddAsync(List<CreateSavedPackagesDto> cSavedPackagesDto);
        Task<string> DeleteAsync(int id);
        Task UpdateAsync(UpdateSavedPackagesDto uSavedPackagesDto);
    }
}
