using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFavAddressesService
    {
        Task<List<FavAddressesDto>> GetAllAsync();
        Task<FavAddressesDto> GetByIdAsync(int id);
        Task<List<FavAddressesDto>> GetByCIdAsync(int cid);
        Task<int> AddAsync(CreateFavAddressesDto cFavAddressesDto);
        Task<string> DeleteAsync(int id);
        Task UpdateAsync(UpdateFavAddressesDto uFavAddressesDto);
    }
}
