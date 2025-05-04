using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AgingReportRepository : IAgingReportRepository
    {
        private readonly OrderDbContext _context;

        public AgingReportRepository(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedListForAgingReport<AgingReport>> GetAgingReportAsync(
            DateTime? fromDate,
            DateTime? toDate,
            //IEnumerable<int> excludedStatusIds,
            string orderNumber,
            int? statusID,
            //IEnumerable<int> includedStatusIds,
            int pageNumber,
            int pageSize)
        {
            var query = _context.orderdetails.AsQueryable();

            //// Apply date filters
            if (fromDate.HasValue)
            {
                query = query.Where(o => o.order_status_change_date >= fromDate.Value);
            }
            if (toDate.HasValue)
            {
                query = query.Where(o => o.order_status_change_date <= toDate.Value);
            }

            //// Apply status filters
            //if (excludedStatusIds != null && excludedStatusIds.Any())
            //{
            //    query = query.Where(o => !excludedStatusIds.Contains((int)o.order_status_id));
            //}
            //if (includedStatusIds != null && includedStatusIds.Any())
            //{
            //    query = query.Where(o => includedStatusIds.Contains((int)o.order_status_id));
            //}

            // Apply order number filter
            if (!string.IsNullOrEmpty(orderNumber))
            {
                query = query.Where(o => o.order_number.Contains(orderNumber));
            }
            if (statusID != null)
            {
                query = query.Where(o => o.order_status_id==statusID);
            }
            var avgProcessingDays = await query
                   .Where(o => o.collection_datetime.HasValue && o.order_status_change_date.HasValue)
                   .AverageAsync(o => (o.collection_datetime.Value - o.order_status_change_date.Value).TotalDays);
            var formattedAvgProcessingTime = $"{Math.Round(avgProcessingDays, 1):0.0} Days";

                    var ordersOver7Days = await query
            .Where(o => o.order_status_change_date.HasValue &&
                       (DateTime.Now - o.order_status_change_date.Value).TotalDays > 7)
            .CountAsync();
      
            // Get total count before pagination
            var totalCount = await query.CountAsync();
            var delayedOrdersCount = await query
                                     .Where(o => o.order_status_change_date.HasValue &&
                                                (DateTime.Now - o.order_status_change_date.Value).TotalDays > 7)
                                     .CountAsync();
            var delayedPercentage = totalCount > 0
                                    ? Math.Round((double)delayedOrdersCount / totalCount * 100, 1)
                                    : 0;
            // First get the raw data from database
            var rawData = await query
                .OrderBy(o => o.order_status_change_date)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new
                {
                    o.consignment_number,
                    o.order_number,
                    o.sender_name,
                    o.order_status_title,
                    o.order_status_change_date,
                    o.suburb_dropoff,
                    o.state_dropoff,
                    o.collection_datetime
                })
                .ToListAsync();

            // Then transform the data in memory
            var items = rawData.Select(o => new AgingReport
            {
                ConsignmentNumber = o.consignment_number,
                OrderNumber = o.order_number,
                SenderName = o.sender_name,
                OrderStatusTitle = o.order_status_title,
                DaysInStatus = o.order_status_change_date.HasValue
                    ? (int)(DateTime.Now - o.order_status_change_date.Value).TotalDays
                    : 0,
                AgingBucket = o.order_status_change_date.HasValue
                    ? GetAgingBucket(o.order_status_change_date.Value)
                    : "Unknown",
                OrderStatusChangeDate = o.order_status_change_date ?? DateTime.MinValue,
                SuburbDropoff = o.suburb_dropoff,
                StateDropoff = o.state_dropoff,
                CollectedDate = o.collection_datetime,

            }).ToList();


            return new PaginatedListForAgingReport<AgingReport>
            {
                Items = items,
                TotalCount = totalCount,
                PageIndex = pageNumber,
                PageSize = pageSize,
                avgProcessingDays= formattedAvgProcessingTime,
                orderOVer7Days=ordersOver7Days,
                delayedPercentage=delayedPercentage
            };
        }

        private static string GetAgingBucket(DateTime statusChangeDate)
        {
            var days = (DateTime.Now - statusChangeDate).TotalDays;

            return days switch
            {
                <= 3 => "0-3 days",
                <= 7 => "4-7 days",
                <= 14 => "8-14 days",
                <= 21 => "15-21 days",
                _ => "22+ days"
            };
        }
    }
}