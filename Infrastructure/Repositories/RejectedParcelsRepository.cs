using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.Repositories
{
    public class RejectedParcelsRepository : IRejectedParcelsRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public readonly DbResource Options; private readonly IConfiguration _configuration;

        public RejectedParcelsRepository(OrderDbContext orderDbContext, IOptions<DbResource> options, IConfiguration configuration)
        {
            _orderDbContext = orderDbContext;
            Options = options.Value; _configuration = configuration;

        }

        public async Task<int> AddAsync(rejectedparcels rp, List<rejectedparcelitems> rpi)
        {
            int rpid = 0;
            using var transaction = await _orderDbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
            try
            {
                List<rejectedparcelitems> rpiList = new List<rejectedparcelitems>();
                await _orderDbContext.rejectedparcels.AddAsync(rp);
                await _orderDbContext.SaveChangesAsync();
                rpid = rp.id;

                foreach (var item in rpi)
                {
                    rejectedparcelitems ri = new rejectedparcelitems();
                    ri.rp_id = rpid;
                    ri.package_type_id = item.package_type_id;
                    ri.package_type = item.package_type;
                    ri.package_content_id = item.package_content_id;
                    ri.package_content = item.package_content;
                    ri.qty = item.qty;
                    ri.weight = item.weight;
                    ri.width = item.width;
                    ri.length = item.length;
                    ri.height = item.height;
                    ri.unit = item.unit;
                    rpiList.Add(ri);
                }

                await _orderDbContext.rejectedparcelitems.AddRangeAsync(rpiList);
                await _orderDbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                // Log the exception
            }
            return rpid;
        }

        public async Task<List<rejectedparcels>> GetAllAsync()
        {
            var data = await _orderDbContext.rejectedparcels.ToListAsync();
            return data;
        }

        public async Task<rejectedparcels> GetAsync(int id) => await _orderDbContext.rejectedparcels.FindAsync(id);

        public async Task<List<rejectedparcelitems>> GetAllItemsByRPIdAsync(int rpid)
        {
            try
            {
                var data = await _orderDbContext.rejectedparcelitems.Where(x => x.rp_id == rpid).ToListAsync();
                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<rp_rpi> GetRPwithItemsByRPIdAsync(int rpid)
        {
            try
            {
                rp_rpi rprpi = new rp_rpi();
                var data = await _orderDbContext.rejectedparcels.Where(x => x.id == rpid).FirstOrDefaultAsync();
                if (data != null)
                {
                    var dataItems = await _orderDbContext.rejectedparcelitems.Where(x => x.rp_id == rpid).ToListAsync();
                    rprpi.rp = data;
                    rprpi.rpi = dataItems;
                }
                return rprpi;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<rejectedparcels>> GetByCIdAsync(int cid)
        {
            var data = await _orderDbContext.rejectedparcels.Where(x => x.customer_id == cid).ToListAsync();
            return data;
        }
        public async Task<PaginatedList<rejectedparcels>> GetByCId_P(int pageIndex, int pageSize,/* int? ordSource,*/ int? opt, string? search, int? customerID)
        {
            try
            {
                var query = _orderDbContext.rejectedparcels.AsQueryable();
                if (customerID != null)
                {
                    query = query.Where(x => x.customer_id == customerID);
                }
                if (!string.IsNullOrEmpty(search) && opt.HasValue)
                {
                    query = opt.Value switch
                    {
                        1 => query.Where(x => x.suburb_pick == search),
                        2 => query.Where(x => x.suburb_drop == search),
                        3 => query.Where(x => x.post_code_pick == search),
                        4 => query.Where(x => x.post_code_drop == search),
                        5 => query.Where(x => x.state_pick == search),
                        6 => query.Where(x => x.state_drop == search),
                        7 => query.Where(x => x.name_sender == search),
                        8 => query.Where(x => x.name_receiver == search),
                        9 => query.Where(x => x.phone_sender == search),
                        10 => query.Where(x => x.phone_receiver == search),
                        11 => query.Where(x => x.email_sender == search),
                        12 => query.Where(x => x.email_receiver == search),
                        _ => query
                    };
                }


                //if (ordSource.HasValue)
                //{

                //}

                var projectedQuery = query
                    //.Select(s => new rejectedparcels
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
                    .OrderByDescending(x => x.id);

                var totalCount = await projectedQuery.CountAsync();
                var items = await projectedQuery
                    .Skip((pageIndex - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PaginatedList<rejectedparcels>
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

        public async Task<bool> DeleteAsync(int id)
        {
            var rp = await _orderDbContext.rejectedparcels.FindAsync(id);
            if (rp != null)
            {
                int rpid = rp.id;
                var rpi = await _orderDbContext.rejectedparcelitems.Where(x => x.rp_id == rpid).ToListAsync();
                _orderDbContext.rejectedparcelitems.RemoveRange(rpi);
                await _orderDbContext.SaveChangesAsync();
                _orderDbContext.rejectedparcels.Remove(rp);
                await _orderDbContext.SaveChangesAsync();
                return true;
            }
            else { return false; }
        }
    }
}
