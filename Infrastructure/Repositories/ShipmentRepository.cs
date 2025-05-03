using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ShipmentRepository:IShipmentRepository
    {
        private readonly OrderDbContext _DbContext;
        public ShipmentRepository(OrderDbContext DbContext)
        {
            _DbContext = DbContext;
        }
        public async Task<List<ordersource>> GetAllOrderSourceAsync() => await _DbContext.ordersource.ToListAsync();
        public async Task<List<options>> GetAllOptionsAsync() => await _DbContext.options.ToListAsync();

        public async Task<PaginatedList<shipment_columns>> GetShipmentAsync(int pageIndex, int pageSize)
        {
            try
            {
                var query = _DbContext.orderdetails
                    .Select(s=> new shipment_columns
                    { 
                     consignment_number=s.consignment_number,
                     reciever_name=s.reciever_name,
                     order_status_title=s.order_status_title,
                     order_number=s.order_number,
                     order_status_change_date=s.order_status_change_date,
                     suburb_dropoff=s.suburb_dropoff
                    })
                    .AsQueryable();
                query = query.OrderByDescending(o => o.order_status_change_date);
                var totalCount = await query.CountAsync();
                var items = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

                return new PaginatedList<shipment_columns>
                {
                    PageIndex = pageIndex,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                    Items = items
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
