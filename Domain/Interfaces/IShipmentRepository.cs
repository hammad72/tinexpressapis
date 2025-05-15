using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IShipmentRepository
    {
        Task<List<ordersource>> GetAllOrderSourceAsync();
        Task<List<options>> GetAllOptionsAsync();
        Task<List<shipment_columns>> getAllShipment(int? ordSource, int? opt, string? search, int? customerID);
        Task<PaginatedList<shipment_columns>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search, int? customerID);
        Task<PaginatedList<orderitems>> getOrderItemsByConsignment(int pageIndex, int pageSize, string consignment);
        SenderRecieverOrderItems getSenderRecieverOrderItems(orderdetails order);
         SummaryOrderItems getSummaryOrderItems(orderdetails order);
         TrackingOrderItems getTrackingOrderItems(orderdetails order);
        CourierInfoOrderItems getCourierInfo(orderdetails order);
    }
}
