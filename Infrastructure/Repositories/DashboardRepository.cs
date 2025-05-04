using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly OrderDbContext _orderDBContext;

        public DashboardRepository(OrderDbContext context)
        {
            _orderDBContext = context;
        }

        public async Task<DashboardStats> GetDashboardStats(string timeFilter)
        {
            var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter);

            var totalOrders = await query.CountAsync();
            if (totalOrders == 0)
            {
                return new DashboardStats();
            }

            var delivered = await query.CountAsync(o => o.order_status_id == 5); // Delivered
            var inTransit = await query.CountAsync(o => o.order_status_id == 2); // In Transit
            var pending = await query.CountAsync(o => o.order_status_id == 1); // Pending
            var failedReturned = await query.CountAsync(o =>
                o.order_status_id == 6 || o.order_status_id == 8); // Failed or Returned

            return new DashboardStats
            {
                TotalOrders = totalOrders,
                Delivered = delivered,
                InTransit = inTransit,
                Pending = pending,
                FailedReturned = failedReturned,
                DeliveredPercentage = CalculatePercentage(delivered, totalOrders),
                InTransitPercentage = CalculatePercentage(inTransit, totalOrders),
                PendingPercentage = CalculatePercentage(pending, totalOrders),
                FailedReturnedPercentage = CalculatePercentage(failedReturned, totalOrders)
            };
        }

        public async Task<List<OrderStatusDistribution>> GetOrderStatusDistribution(string timeFilter)
        {
            var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter);

            return await query
                .GroupBy(o => new { o.order_status_id, o.order_status_title })
                .Select(g => new OrderStatusDistribution
                {
                    Status = g.Key.order_status_title ?? "Unknown",
                    Count = g.Count()
                })
                .Where(x => x.Status != "Unknown")
                .ToListAsync();
        }
        public async Task<List<DailyOrderTrend>> GetDailyOrderTrend(string timeFilter)
        {
            var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter)
                .Where(o => o.order_status_change_date != null);

            if (timeFilter == "weekly")
            {
                var weeklyData = await query
                    .GroupBy(o => o.order_status_change_date.Value.DayOfWeek)
                    .Select(g => new
                    {
                        DayOfWeek = g.Key,
                        Delivered = g.Count(o => o.order_status_id == 5),
                        InTransit = g.Count(o => o.order_status_id == 2),
                        Pending = g.Count(o => o.order_status_id == 1),
                        Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                    })
                    .ToListAsync();

                return weeklyData.Select(x => new DailyOrderTrend
                {
                    Day = x.DayOfWeek.ToString(),
                    Delivered = x.Delivered,
                    InTransit = x.InTransit,
                    Pending = x.Pending,
                    Failed = x.Failed
                }).ToList();
            }

            // Default to daily trend for last 7 days
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-7);

            var dailyData = await query
                .Where(o => o.order_status_change_date >= startDate &&
                           o.order_status_change_date <= endDate)
                .GroupBy(o => o.order_status_change_date.Value.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Delivered = g.Count(o => o.order_status_id == 5),
                    InTransit = g.Count(o => o.order_status_id == 2),
                    Pending = g.Count(o => o.order_status_id == 1),
                    Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                })
                .ToListAsync();

            return dailyData.Select(x => new DailyOrderTrend
            {
                Day = x.Date.ToString("ddd"),
                Delivered = x.Delivered,
                InTransit = x.InTransit,
                Pending = x.Pending,
                Failed = x.Failed
            }).OrderBy(x => x.Day).ToList();
        }
        //public async Task<List<DailyOrderTrend>> GetDailyOrderTrend(string timeFilter)
        //{
        //    var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter)
        //        .Where(o => o.order_status_change_date != null);

        //    if (timeFilter == "weekly")
        //    {
        //        return await query
        //            .GroupBy(o => o.order_status_change_date.Value.DayOfWeek)
        //            .Select(g => new DailyOrderTrend
        //            {
        //                Day = g.Key.ToString(),
        //                Delivered = g.Count(o => o.order_status_id == 5),
        //                InTransit = g.Count(o => o.order_status_id == 2),
        //                Pending = g.Count(o => o.order_status_id == 1),
        //                Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
        //            })
        //            .OrderBy(d => d.Day)
        //            .ToListAsync();
        //    }

        //    // Default to daily trend for last 7 days
        //    var endDate = DateTime.Today;
        //    var startDate = endDate.AddDays(-7);

        //    return await query
        //        .Where(o => o.order_status_change_date >= startDate &&
        //                   o.order_status_change_date <= endDate)
        //        .GroupBy(o => o.order_status_change_date.Value.Date)
        //        .Select(g => new DailyOrderTrend
        //        {
        //            Day = g.Key.ToString("ddd"),
        //            Delivered = g.Count(o => o.order_status_id == 5),
        //            InTransit = g.Count(o => o.order_status_id == 2),
        //            Pending = g.Count(o => o.order_status_id == 1),
        //            Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
        //        })
        //        .OrderBy(d => d.Day)
        //        .ToListAsync();
        //}


        public async Task<List<RecentOrder>> GetRecentOrders(string timeFilter)
        {
            // First get the raw data from database
            var rawData = await ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter)
                .Where(o => o.order_status_change_date != null)
                .OrderByDescending(o => o.order_status_change_date)
                .Take(10)
                .Select(o => new
                {
                    o.order_number,
                    o.sender_name,
                    o.suburb_dropoff,
                    o.state_dropoff,
                    o.order_status_title,
                    o.order_status_change_date
                })
                .ToListAsync();

            // Then format the data in memory
            return rawData.Select(o => new RecentOrder
            {
                OrderId = o.order_number ?? "N/A",
                Customer = o.sender_name ?? "Unknown",
                Destination = FormatDestination(o.suburb_dropoff, o.state_dropoff),
                Status = o.order_status_title ?? "Unknown",
                LastUpdate = o.order_status_change_date.HasValue
                    ? o.order_status_change_date.Value.ToString("MMM dd, hh:mm tt")
                    : "N/A",
                Progress = GetProgressPercentage(o.order_status_title)
            }).ToList();
        }

        private IQueryable<orderdetails> ApplyTimeFilter(IQueryable<orderdetails> query, string timeFilter)
        {
            var now = DateTime.Now;
            query = query.Where(o => o.order_status_change_date != null);

            return timeFilter switch
            {
                "today" => query.Where(o => o.order_status_change_date.Value.Date == now.Date),
                "weekly" => query.Where(o => o.order_status_change_date.Value >= now.AddDays(-7)),
                "monthly" => query.Where(o => o.order_status_change_date.Value.Month == now.Month &&
                                            o.order_status_change_date.Value.Year == now.Year),
                "yearly" => query.Where(o => o.order_status_change_date.Value.Year == now.Year),
                _ => query // "overall" or default
            };
        }
        private decimal CalculatePercentage(int count, int total)
        {
            return total == 0 ? 0 : Math.Round((decimal)count / total * 100, 1);
        }

        private static string FormatDestination(string suburb, string state)
        {
            return string.IsNullOrEmpty(suburb) && string.IsNullOrEmpty(state)
                ? "N/A"
                : $"{suburb ?? ""}, {state ?? ""}".Trim(',', ' ');
        }

        private static int GetProgressPercentage(string status)
        {
            return status switch
            {
                "Pending" => 25,
                "In Transit" => 50,
                "Out for Delivery" => 75,
                "Delivered" => 100,
                "Failed" or "Returned" => 100,
                _ => 0
            };
        }
   
    }
}