using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomerPriorityService
    {
        Task<List<CustomerPriorityDto>> GetAllAsync();
        Task<CustomerPriorityDto> GetByIdAsync(int id);
        Task<List<CustomerPriorityDto>> GetByCIdAsync(int id);
        Task<bool> AddAsync(List<CreateCustomerPriorityDto> cDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateCustomerPriorityDto uDto);
    }
}
