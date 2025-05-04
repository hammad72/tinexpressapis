using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderStatusesService
    {
        Task<List<OrderStatusesDto>> GetAllAsync();
        Task<OrderStatusesDto> GetByIdAsync(int id);
        Task<int> AddAsync(CreateOrderStatusesDto cDto);
        Task DeleteAsync(int id);
        Task UpdateAsync(UpdateOrderStatusesDto uDto);
    }
}
