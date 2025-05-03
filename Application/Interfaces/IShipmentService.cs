using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShipmentService
    {
        Task<List<OrderSourceDto>> GetAllOrderSourceAsync();
        Task<List<OptionsDto>> GetAllOptionsAsync();
        Task<PaginatedList<shipmentDto>> GetShipmentAsync(int pageIndex, int pageSize);

    }
}
