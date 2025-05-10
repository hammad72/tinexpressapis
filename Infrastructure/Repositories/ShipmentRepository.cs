using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
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

        public async Task<List<shipment_columns>> getAllShipment(int? ordSource, int? opt, string? search, int? customerID)
        {
            try
            {
                var query = _DbContext.orderdetails.AsQueryable();
                if (customerID != null)
                {
                    query = query.Where(x => x.customer_id == customerID);
                }
                if (!string.IsNullOrEmpty(search) && opt.HasValue)
                {
                    query = opt.Value switch
                    {
                        1 => query.Where(x => x.order_number == search),
                        2 => query.Where(x => x.consignment_number == search),
                        3 => query.Where(x => x.order_status_title == search),
                        4 => query.Where(x => x.courier_title == search),
                        5 => query.Where(x => x.suburb_pickup == search),
                        6 => query.Where(x => x.sender_email == search),
                        7 => query.Where(x => x.suburb_dropoff == search),
                        8 => query.Where(x => x.reciever_email == search),
                        _ => query
                    };
                }


                if (ordSource.HasValue)
                {

                }

                var projectedQuery = query.Select(s => new shipment_columns
                {
                    consignment_number = s.consignment_number,
                    reciever_name = s.reciever_name,
                    order_status_title = s.order_status_title,
                    order_number = s.order_number,
                    order_status_change_date = s.order_status_change_date,
                    suburb_dropoff = s.suburb_dropoff,
                    courier = s.courier_title,
                    suburb_pickup = s.suburb_pickup
                })
                .OrderByDescending(o => o.order_status_change_date)
                .ToList();
                return projectedQuery;
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw; // Re-throw the exception after logging
            }
        }
        public async Task<PaginatedList<shipment_columns>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search, int? customerID)
        {
            try
            {
                var query = _DbContext.orderdetails.AsQueryable();
                if (customerID != null)
                {
                    query = query.Where(x => x.customer_id == customerID);
                }
                if (!string.IsNullOrEmpty(search) && opt.HasValue)
                {
                    query = opt.Value switch
                    {
                        1 => query.Where(x => x.order_number == search),
                        2 => query.Where(x => x.consignment_number == search),
                        3 => query.Where(x => x.order_status_title == search),
                        4 => query.Where(x => x.courier_title == search),
                        5 => query.Where(x => x.suburb_pickup == search),
                        6 => query.Where(x => x.sender_email == search),
                        7 => query.Where(x => x.suburb_dropoff == search),
                        8 => query.Where(x => x.reciever_email == search),
                        _ => query
                    };
                }


                if (ordSource.HasValue) 
                {

                }

                var projectedQuery = query.Select(s => new shipment_columns
                {
                    consignment_number = s.consignment_number,
                    reciever_name = s.reciever_name,
                    order_status_title = s.order_status_title,
                    order_number = s.order_number,
                    order_status_change_date = s.order_status_change_date,
                    suburb_dropoff = s.suburb_dropoff,
                    courier = s.courier_title,
                    suburb_pickup = s.suburb_pickup
                })
                .OrderByDescending(o => o.order_status_change_date);

                var totalCount = await projectedQuery.CountAsync();
                var items = await projectedQuery
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

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
                // Log the exception here
                throw; // Re-throw the exception after logging
            }
        }

        public async Task<PaginatedList<orderitems>> getOrderItemsByConsignment(int pageIndex, int pageSize, string consignment)
        {
            try
            {
                var query = _DbContext.orderitems.AsQueryable();

                query = query.Where(x => x.consignment_number == consignment);
                //var projectedQuery = query.Select(s => new shipment_columns
                //{
                //    consignment_number = s.consignment_number,
                //    reciever_name = s.reciever_name,
                //    order_status_title = s.order_status_title,
                //    order_number = s.order_number,
                //    order_status_change_date = s.order_status_change_date,
                //    suburb_dropoff = s.suburb_dropoff,
                //    courier = s.courier_title,
                //    suburb_pickup = s.suburb_pickup
                //})
                //.OrderByDescending(o => o.order_status_change_date);

                var projectedQuery = query.OrderByDescending(o => o.id);
                var totalCount = await projectedQuery.CountAsync();
                var items = await projectedQuery
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<orderitems>
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
                // Log the exception here
                throw; // Re-throw the exception after logging
            }
        }

        public  SenderRecieverOrderItems getSenderRecieverOrderItems(orderdetails order)
        {
            try
            {
                //var query = _DbContext.orderdetails.AsQueryable();

                //var order = await query
                //    .Where(x => x.consignment_number == consignment)
                //    .FirstOrDefaultAsync();

                if (order == null)
                {
               
                    return null; 
                }

                return new SenderRecieverOrderItems
                {
                    sender_name = order.sender_name,  
                    sender_email = order.sender_email,
                    sender_phone = order.sender_phone,
                    reciever_name = order.reciever_name,
                    reciever_email = order.reciever_email,
                    reciever_phone = order.reciever_phone,
                    address_pickup = order.address_pickup,
                    address_dropoff = order.address_dropoff
                };
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public  SummaryOrderItems getSummaryOrderItems(orderdetails order)
        {
            try
            {
                //var query = _DbContext.orderdetails.AsQueryable();

                //var ordDetail = await query
                //    .Where(x => x.consignment_number == consignment)
                //    .FirstOrDefaultAsync();

                if (order == null )
                {
                    return null;
                }

                return new SummaryOrderItems
                {
                    quote_price = order.quote_price,
                    sale_tax = order.sale_tax,
                    price = order.price,
                    net_price = order.net_price
                };
            }
            catch (Exception ex)
            {
                // Consider logging the exception here
                return null;
            }
        }

        public TrackingOrderItems getTrackingOrderItems(orderdetails order)
        {
            //var orderD = order.order_status_change_date;
            return new TrackingOrderItems
            {
                order_status_title=order.order_status_title,
                order_status_change_date = order.order_status_change_date
            };
        }

  

    }
}
