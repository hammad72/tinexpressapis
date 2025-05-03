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
        Task<PaginatedList<shipment_columns>> GetShipmentAsync( int pageNumber, int pageSize);
        //Task<PaginatedList<orderdetails>> GetShipmentAsync(string orderSource, string option, string search, int pageNumber, int pageSize);
    }
}
