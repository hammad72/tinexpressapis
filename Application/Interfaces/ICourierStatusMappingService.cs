using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICourierStatusMappingService
    {
        Task<List<CourierStatusMappingDto>> GetAllAsync();
        Task<CourierStatusMappingDto> GetByIdAsync(int id);
        Task<List<CourierStatusMappingDto>> GetByCIdAsync(int cid);
        Task<bool> AddAsync(List<CreateCourierStatusMappingDto> cDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCourierStatusMappingDto uDto);
    }
}
