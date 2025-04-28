using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICouriersService
    {
        Task<List<CouriersDto>> GetAllAsync();
        Task<CouriersDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateCouriersDto cCouriersDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCouriersDto uCouriersDto);
    }
}
