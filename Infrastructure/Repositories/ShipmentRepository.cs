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

        public async Task<List<shipment_columns>> getAllShipment(int? ordSource, int? opt, string? search)
        {
            try
            {
                var query = _DbContext.orderdetails.AsQueryable();

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
                    suburb_dropoff = s.suburb_dropoff
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
        public async Task<PaginatedList<shipment_columns>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search)
        {
            try
            {
                var query = _DbContext.orderdetails.AsQueryable();

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
                    suburb_dropoff = s.suburb_dropoff
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
        //public async Task<PaginatedList<shipment_columns>> GetShipmentAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search)
        //{
        //    try
        //    {

        //        var query = _DbContext.orderdetails
        //            //.Select(s=> new shipment_columns
        //            //{ 
        //            // consignment_number=s.consignment_number,
        //            // reciever_name=s.reciever_name,
        //            // order_status_title=s.order_status_title,
        //            // order_number=s.order_number,
        //            // order_status_change_date=s.order_status_change_date,
        //            // suburb_dropoff=s.suburb_dropoff
        //            //})
        //            .AsQueryable();
        //        var data=new List<shipment_columns>();
        //        if (!string.IsNullOrEmpty(search))
        //        {
        //            if (ordSource == 1) //for call origin ordersource no column present
        //            {

        //            }
        //            else if (ordSource == 2)
        //            {

        //            }
        //            else if (ordSource == 3)
        //            {

        //            }
        //            else if (ordSource == 4)
        //            {

        //            }
        //            else if (ordSource == 5)
        //            {

        //            }
        //            if (opt == 1)
        //            {
        //                data = query.Where(x => x.order_number == search)
        //                    .Select(s => new shipment_columns
        //                    {
        //                        consignment_number = s.consignment_number,
        //                        reciever_name = s.reciever_name,
        //                        order_status_title = s.order_status_title,
        //                        order_number = s.order_number,
        //                        order_status_change_date = s.order_status_change_date,
        //                        suburb_dropoff = s.suburb_dropoff
        //                    }).ToList();
        //            }
        //            else if (opt == 2)
        //            {
        //                data = query.Where(x => x.consignment_number == search)
        //                       .Select(s => new shipment_columns
        //                       {
        //                           consignment_number = s.consignment_number,
        //                           reciever_name = s.reciever_name,
        //                           order_status_title = s.order_status_title,
        //                           order_number = s.order_number,
        //                           order_status_change_date = s.order_status_change_date,
        //                           suburb_dropoff = s.suburb_dropoff
        //                       }).ToList();
        //            }
        //            else if (opt == 3)
        //            {
        //                data = query.Where(x => x.order_status_title == search)
        //                      .Select(s => new shipment_columns
        //                      {
        //                          consignment_number = s.consignment_number,
        //                          reciever_name = s.reciever_name,
        //                          order_status_title = s.order_status_title,
        //                          order_number = s.order_number,
        //                          order_status_change_date = s.order_status_change_date,
        //                          suburb_dropoff = s.suburb_dropoff
        //                      }).ToList();

        //            }
        //            else if (opt == 4)
        //            {
        //                data = query.Where(x => x.courier_title == search)
        //                 .Select(s => new shipment_columns
        //                 {
        //                     consignment_number = s.consignment_number,
        //                     reciever_name = s.reciever_name,
        //                     order_status_title = s.order_status_title,
        //                     order_number = s.order_number,
        //                     order_status_change_date = s.order_status_change_date,
        //                     suburb_dropoff = s.suburb_dropoff
        //                 }).ToList();

        //            }
        //            else if (opt == 5)
        //            {

        //                data = query.Where(x => x.suburb_pickup == search)
        //                 .Select(s => new shipment_columns
        //                 {
        //                     consignment_number = s.consignment_number,
        //                     reciever_name = s.reciever_name,
        //                     order_status_title = s.order_status_title,
        //                     order_number = s.order_number,
        //                     order_status_change_date = s.order_status_change_date,
        //                     suburb_dropoff = s.suburb_dropoff
        //                 }).ToList();
        //            }
        //            else if (opt == 6)
        //            {

        //                data = query.Where(x => x.sender_email == search)
        //                 .Select(s => new shipment_columns
        //                 {
        //                     consignment_number = s.consignment_number,
        //                     reciever_name = s.reciever_name,
        //                     order_status_title = s.order_status_title,
        //                     order_number = s.order_number,
        //                     order_status_change_date = s.order_status_change_date,
        //                     suburb_dropoff = s.suburb_dropoff
        //                 }).ToList();
        //            }
        //            else if (opt == 7)
        //            {

        //                data = query.Where(x => x.suburb_dropoff == search)
        //                 .Select(s => new shipment_columns
        //                 {
        //                     consignment_number = s.consignment_number,
        //                     reciever_name = s.reciever_name,
        //                     order_status_title = s.order_status_title,
        //                     order_number = s.order_number,
        //                     order_status_change_date = s.order_status_change_date,
        //                     suburb_dropoff = s.suburb_dropoff
        //                 }).ToList();
        //            }
        //            else if (opt == 8)
        //            {

        //                data = query.Where(x => x.reciever_email == search)
        //                 .Select(s => new shipment_columns
        //                 {
        //                     consignment_number = s.consignment_number,
        //                     reciever_name = s.reciever_name,
        //                     order_status_title = s.order_status_title,
        //                     order_number = s.order_number,
        //                     order_status_change_date = s.order_status_change_date,
        //                     suburb_dropoff = s.suburb_dropoff
        //                 }).ToList();

        //            }
        //        }
        //        data = query
        //          .Select(s => new shipment_columns
        //          {
        //              consignment_number = s.consignment_number,
        //              reciever_name = s.reciever_name,
        //              order_status_title = s.order_status_title,
        //              order_number = s.order_number,
        //              order_status_change_date = s.order_status_change_date,
        //              suburb_dropoff = s.suburb_dropoff
        //          })
        //          .OrderByDescending(o => o.order_status_change_date)
        //          .ToList();
        //        //query = query.OrderByDescending(o => o.order_status_change_date);
        //        var totalCount =  data.Count();
        //        //var items = await data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        //        var items =  data.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        //        return new PaginatedList<shipment_columns>
        //        {
        //            PageIndex = pageIndex,
        //            PageSize = pageSize,
        //            TotalCount = totalCount,
        //            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        //            Items = items
        //        };
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}
    }
}
