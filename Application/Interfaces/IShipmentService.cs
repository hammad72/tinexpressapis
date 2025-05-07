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
        Task<List<shipmentDto>> getAllShipment(int? ordSource, int? opt, string? search, int? customerID);
        Task<PaginatedList<shipmentDto>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search,int? customerID);
        Task<byte[]> ExportShipmentsToCsv(int? ordSource, int? opt, string? search, int? customerID);
        Task<byte[]> ExportShipmentsToExcel(int? ordSource, int? opt, string? search, int? customerID);
        Task<ShipmentDetailOrderItemsDTO> getShipmentItemsAsync(int pageIndex, int pageSize, string consignment);
        Task<PaginatedList<OrderItemsDto>> getOrderItemsByConsignment(int pageIndex, int pageSize, string consignment);
    }
}
