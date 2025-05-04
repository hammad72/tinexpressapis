using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICourierStatusesService
    {
        Task<List<CourierStatusesDto>> GetAllAsync();
        Task<CourierStatusesDto> GetByIdAsync(int id);
        Task<List<CourierStatusesDto>> GetByCIdAsync(int cid);
        Task<int> AddAsync(CreateCourierStatusesDto cDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCourierStatusesDto uDto);
    }
}
