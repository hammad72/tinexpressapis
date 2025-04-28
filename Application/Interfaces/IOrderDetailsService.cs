using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderDetailsService
    {
        Task<List<OrderDetailsDto>> GetAllAsync();
        Task<OrderDetailsDto> GetByIdAsync(int id);
        Task<string> AddAsync(OrderDetailsDto cOrderDetailDto, List<OrderItemsDto> cOrderItemsDto);
        //Task DeleteAsync(int id);
        //Task UpdateAsync(UpdateCouriersDto uCouriersDto);
    }
}
