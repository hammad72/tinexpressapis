using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<DashboardStats> GetDashboardStats(string timeFilter, string dashType, int? customerId)
        {
            var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter,  dashType, customerId);

            var totalOrders = await query.CountAsync();
            if (totalOrders == 0)
            {
                return new DashboardStats();
            }
            ////"Pending" => 25,
            //1 => 25,
            //    3 => 50,
            //    //"In Transit" => 50,
            //    //"Out for Delivery" => 75,
            //    4 => 75,
            //    //"Delivered" => 100,
            //    5 => 100,
            //    //"Failed" or "Returned" => 100,
            //    6 or 8 => 100,
            //    _ => 0
            var delivered = await query.CountAsync(o => o.order_status_id == 5); // Delivered
            var inTransit = await query.CountAsync(o => o.order_status_id == 3); // In Transit
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

        public async Task<List<OrderStatusDistribution>> GetOrderStatusDistribution(string timeFilter, string dashType, int? customerId)
        {
            var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter,  dashType, customerId);
            var getD= await query
                .GroupBy(o => new { o.order_status_id, o.order_status_title })
                .Select(g => new OrderStatusDistribution
                {
                    id = (int)g.Key.order_status_id,
                    Status = g.Key.order_status_title ?? "Unknown",
                    Count = g.Count()
                })
                //.Where(x => x.id != null)
                .ToListAsync();
            var failedIds = new[] { 6, 7, 8, 9 };

            var combinedFailedCount = getD
                .Where(x => failedIds.Contains(x.id))
                .Sum(x => x.Count);
            getD.RemoveAll(x => failedIds.Contains(x.id));
            getD.Add(new OrderStatusDistribution
            {
                id = 6, // you can choose a common ID
                Status = "Failed",
                Count = combinedFailedCount
            });
         
         
            return getD;

        }
        public async Task<List<DailyOrderTrend>> GetDailyOrderTrend(string timeFilter, string dashType, int? customerId)
        {
            var query = ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter,  dashType,  customerId)
                .Where(o => o.order_status_change_date != null);

            switch (timeFilter.ToLower())
            {
                case "today":
                    return await GetHourlyTrend(query);
                case "weekly":
                    return await GetWeeklyTrend(query);
                case "monthly":
                    return await GetMonthlyTrend(query);
                case "yearly":
                    return await GetYearlyTrend(query);
                default:
                    return await GetDefaultTrend(query);
            }
        }

        private async Task<List<DailyOrderTrend>> GetHourlyTrend(IQueryable<orderdetails> query)
        {
            try
            {
                var now = DateTime.Now;
                var startDate = now.Date;

                // First get the data from database without the formatted string
                var dbResult = await query
                    .Where(o => o.order_status_change_date >= startDate)
                    .GroupBy(o => new { Hour = o.order_status_change_date.Value.Hour / 2 })
                    .Select(g => new
                    {
                        Hour = g.Key.Hour,
                        OrderBooked = g.Count(o => o.order_status_id == 1),
                        Collected = g.Count(o => o.order_status_id == 2),
                        AwaitingDispatch = g.Count(o => o.order_status_id == 3),
                        OutForDelivery = g.Count(o => o.order_status_id == 4),
                        Delivered = g.Count(o => o.order_status_id == 5),
                        Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 7 || o.order_status_id == 8 || o.order_status_id == 9)
                    })
                    .OrderBy(x => x.Hour)
                    .ToListAsync();

                // Then format the strings in memory
                var result = dbResult.Select(x => new DailyOrderTrend
                {
                    Day = $"{x.Hour * 2}:00-{(x.Hour * 2) + 2}:00",
                    OrderBooked = x.OrderBooked,
                    Collected = x.Collected,
                    AwaitingDispatch = x.AwaitingDispatch,
                    OutForDelivery = x.OutForDelivery,
                    Delivered = x.Delivered,
                    Failed = x.Failed
                }).ToList();

                return result;
                //var now = DateTime.Now;
                //var startDate = now.Date;
                //var que= await query
                //    .Where(o => o.order_status_change_date >= startDate)
                //    .GroupBy(o => new { Hour = o.order_status_change_date.Value.Hour / 2 })
                //    .Select(g => new DailyOrderTrend
                //    {
                //        Day = $"{g.Key.Hour * 2}:00-{(g.Key.Hour * 2) + 2}:00",
                //        OrderBooked = g.Count(o => o.order_status_id == 1),
                //        Collected = g.Count(o => o.order_status_id == 2),
                //        AwaitingDispatch = g.Count(o => o.order_status_id == 3),
                //        OutForDelivery = g.Count(o => o.order_status_id == 4),
                //        Delivered = g.Count(o => o.order_status_id == 5),
                //        Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 7 || o.order_status_id == 8 || o.order_status_id == 9),
                //        //Delivered = g.Count(o => o.order_status_id == 5),
                //        //InTransit = g.Count(o => o.order_status_id == 2),
                //        //Pending = g.Count(o => o.order_status_id == 1),
                //        //Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                //    })
                //    .OrderBy(x => x.Day)
                //    .ToListAsync();
                //return que;
            }
            catch (Exception)
            {

                throw;
            }

      
        }

        private async Task<List<DailyOrderTrend>> GetWeeklyTrend(IQueryable<orderdetails> query)
        {
            var weeklyData = await query
                .GroupBy(o => o.order_status_change_date.Value.DayOfWeek)
                .Select(g => new
                {
                    DayOfWeek = g.Key,
                    OrderBooked = g.Count(o => o.order_status_id == 1),
                    Collected = g.Count(o => o.order_status_id == 2),
                    AwaitingDispatch = g.Count(o => o.order_status_id == 3),
                    OutForDelivery = g.Count(o => o.order_status_id == 4),
                    Delivered = g.Count(o => o.order_status_id == 5),
                    Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 7 || o.order_status_id == 8 || o.order_status_id == 9),
                    //Delivered = g.Count(o => o.order_status_id == 5),
                    //InTransit = g.Count(o => o.order_status_id == 2),
                    //Pending = g.Count(o => o.order_status_id == 1),
                    //Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                })
                .ToListAsync();

            // Ensure all days of week are present
            var allDays = Enum.GetValues(typeof(DayOfWeek)).Cast<DayOfWeek>();
            return allDays.Select(day =>
            {
                var dayData = weeklyData.FirstOrDefault(d => d.DayOfWeek == day);
                return new DailyOrderTrend
                {
                    Day = day.ToString(),
                    OrderBooked = dayData?.OrderBooked ?? 0,
                    Collected = dayData?.Collected ?? 0,
                    AwaitingDispatch = dayData?.AwaitingDispatch ?? 0,
                    OutForDelivery = dayData?.OutForDelivery ?? 0,
                    Delivered = dayData?.Delivered ?? 0,
                    Failed = dayData?.Failed ?? 0
                    //Delivered = dayData?.Delivered ?? 0,
                    //InTransit = dayData?.InTransit ?? 0,
                    //Pending = dayData?.Pending ?? 0,
                    //Failed = dayData?.Failed ?? 0
                };
            }).ToList();
        }

        private async Task<List<DailyOrderTrend>> GetMonthlyTrend(IQueryable<orderdetails> query)
        {
            var now = DateTime.Now;
            var daysInMonth = DateTime.DaysInMonth(now.Year, now.Month);

            var monthlyData = await query
                .GroupBy(o => o.order_status_change_date.Value.Day)
                .Select(g => new
                {
                    Day = g.Key,
                    OrderBooked = g.Count(o => o.order_status_id == 1),
                    Collected = g.Count(o => o.order_status_id == 2),
                    AwaitingDispatch = g.Count(o => o.order_status_id == 3),
                    OutForDelivery = g.Count(o => o.order_status_id == 4),
                    Delivered = g.Count(o => o.order_status_id == 5),
                    Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 7 || o.order_status_id == 8 || o.order_status_id == 9),
                    //Delivered = g.Count(o => o.order_status_id == 5),
                    //InTransit = g.Count(o => o.order_status_id == 2),
                    //Pending = g.Count(o => o.order_status_id == 1),
                    //Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                })
                .ToListAsync();

            // Ensure all days of month are present
            return Enumerable.Range(1, daysInMonth).Select(day =>
            {
                var dayData = monthlyData.FirstOrDefault(d => d.Day == day);
                return new DailyOrderTrend
                {
                    Day = day.ToString(),
                    OrderBooked = dayData?.OrderBooked ?? 0,
                    Collected = dayData?.Collected ?? 0,
                    AwaitingDispatch = dayData?.AwaitingDispatch ?? 0,
                    OutForDelivery = dayData?.OutForDelivery ?? 0,
                    Delivered = dayData?.Delivered ?? 0,
                    Failed = dayData?.Failed ?? 0
                    //Delivered = dayData?.Delivered ?? 0,
                    //InTransit = dayData?.InTransit ?? 0,
                    //Pending = dayData?.Pending ?? 0,
                    //Failed = dayData?.Failed ?? 0
                };
            }).ToList();
        }

        private async Task<List<DailyOrderTrend>> GetYearlyTrend(IQueryable<orderdetails> query)
        {
            var yearlyData = await query
                .GroupBy(o => new { Month = o.order_status_change_date.Value.Month })
                .Select(g => new
                {
                    Month = g.Key.Month,
                    OrderBooked = g.Count(o => o.order_status_id == 1),
                    Collected = g.Count(o => o.order_status_id == 2),
                    AwaitingDispatch = g.Count(o => o.order_status_id == 3),
                    OutForDelivery = g.Count(o => o.order_status_id == 4),
                    Delivered = g.Count(o => o.order_status_id == 5),
                    Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 7 || o.order_status_id == 8 || o.order_status_id == 9),
                    //Delivered = g.Count(o => o.order_status_id == 5),
                    //InTransit = g.Count(o => o.order_status_id == 2),
                    //Pending = g.Count(o => o.order_status_id == 1),
                    //Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                })
                .ToListAsync();

            // Ensure all months are present
            return Enumerable.Range(1, 12).Select(month =>
            {
                var monthData = yearlyData.FirstOrDefault(m => m.Month == month);
                return new DailyOrderTrend
                {
                    Day = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month),
                    OrderBooked = monthData?.OrderBooked ?? 0,
                    Collected = monthData?.Collected ?? 0,
                    AwaitingDispatch = monthData?.AwaitingDispatch ?? 0,
                    OutForDelivery = monthData?.OutForDelivery ?? 0,
                    Delivered = monthData?.Delivered ?? 0,
                    Failed = monthData?.Failed ?? 0
                    //Delivered = monthData?.Delivered ?? 0,
                    //InTransit = monthData?.InTransit ?? 0,
                    //Pending = monthData?.Pending ?? 0,
                    //Failed = monthData?.Failed ?? 0
                };
            }).ToList();
        }

        private async Task<List<DailyOrderTrend>> GetDefaultTrend(IQueryable<orderdetails> query)
        {
            // Default to last 7 days
            var endDate = DateTime.Today;
            var startDate = endDate.AddDays(-7);

            return await query
                .Where(o => o.order_status_change_date >= startDate &&
                           o.order_status_change_date <= endDate)
                .GroupBy(o => o.order_status_change_date.Value.Date)
                .Select(g => new DailyOrderTrend
                {
                    Day = g.Key.ToString("ddd"),
                    OrderBooked = g.Count(o => o.order_status_id == 1),
                    Collected = g.Count(o => o.order_status_id == 2),
                    AwaitingDispatch = g.Count(o => o.order_status_id == 3),
                    OutForDelivery = g.Count(o => o.order_status_id == 4),
                    Delivered = g.Count(o => o.order_status_id == 5),
                    Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 7 || o.order_status_id == 8 || o.order_status_id == 9),
                    //Delivered = g.Count(o => o.order_status_id == 5),
                    //InTransit = g.Count(o => o.order_status_id == 2),
                    //Pending = g.Count(o => o.order_status_id == 1),
                    //Failed = g.Count(o => o.order_status_id == 6 || o.order_status_id == 8)
                })
                .OrderBy(d => d.Day)
                .ToListAsync();
        }


        //public async Task<PaginatedList<RecentOrder>> GetRecentOrders(string timeFilter,int ordStatusID, string barValue, int pageIndex, int pageSize)
        //{
        //    // First get the raw data from database
        //        var rawData = new List<RecentOrder>();
        //        if (ordStatusID==6 || ordStatusID == 7 || ordStatusID == 8 || ordStatusID == 9)
        //        {
        //            var integers = new List<int> { 6, 7, 8, 9 };
        //            rawData = await ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter)
        //                    .Where(o => integers.Contains((int)o.order_status_id))
        //                    .OrderByDescending(o => o.order_status_change_date)
        //                    //.Take(10)
        //                    .Select(o => new RecentOrder
        //                    {
        //                    OrderId = o.order_number ?? "N/A",
        //                    Customer = o.sender_name ?? "Unknown",
        //                    Destination = FormatDestination(o.suburb_dropoff, o.state_dropoff),
        //                    Status = o.order_status_title ?? "Unknown",
        //                    LastUpdate = o.order_status_change_date.HasValue
        //                    ? o.order_status_change_date.Value.ToString("MMM dd, hh:mm tt")
        //                    : "N/A",
        //                    Progress = GetProgressPercentage((int)o.order_status_id)
        //                    })
        //                    .ToListAsync();

        //        }
        //        else
        //        {
        //            rawData = await ApplyTimeFilter(_orderDBContext.orderdetails, timeFilter)
        //                    .Where(o => o.order_status_id == ordStatusID)
        //                    .OrderByDescending(o => o.order_status_change_date)
        //                    //.Take(10)
        //                    .Select(o => new RecentOrder
        //                    {
        //                    OrderId = o.order_number ?? "N/A",
        //                    Customer = o.sender_name ?? "Unknown",
        //                    Destination = FormatDestination(o.suburb_dropoff, o.state_dropoff),
        //                    Status = o.order_status_title ?? "Unknown",
        //                    LastUpdate = o.order_status_change_date.HasValue
        //                    ? o.order_status_change_date.Value.ToString("MMM dd, hh:mm tt")
        //                    : "N/A",
        //                    Progress = GetProgressPercentage((int)o.order_status_id)
        //                    })
        //                    .ToListAsync();
        //        }


        //        var totalCount = rawData.Count();
        //        var items =  rawData
        //                    .Skip((pageIndex - 1) * pageSize)
        //                    .Take(pageSize)
        //                    .ToList();

        //                    return new PaginatedList<RecentOrder>
        //                    {
        //                    PageIndex = pageIndex,
        //                    PageSize = pageSize,
        //                    TotalCount = totalCount,
        //                    TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
        //                    Items = items
        //                    };
        //    // Then format the data in memory
        //    //return rawData.Select(o => new RecentOrder
        //    //{
        //    //    OrderId = o.order_number ?? "N/A",
        //    //    Customer = o.sender_name ?? "Unknown",
        //    //    Destination = FormatDestination(o.suburb_dropoff, o.state_dropoff),
        //    //    Status = o.order_status_title ?? "Unknown",
        //    //    LastUpdate = o.order_status_change_date.HasValue
        //    //        ? o.order_status_change_date.Value.ToString("MMM dd, hh:mm tt")
        //    //        : "N/A",
        //    //    Progress = GetProgressPercentage(o.order_status_title)
        //    //}).ToList();
        //}

        private IQueryable<orderdetails> ApplyTimeFilter(IQueryable<orderdetails> query, string timeFilter,string dashType,int? customerId)
        {
            if (dashType == "Admin")
            {
              
                query = query.Where(o => o.order_status_change_date != null);
            }
            else if (dashType =="Customer")
            {
        
                query = query.Where(o => o.order_status_change_date != null && o.courier_id==customerId );//need to verify this logic where is the customer id
            }
            var now = DateTime.Now;

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

        private static int GetProgressPercentage(int status)
        {
            return status switch
            {
                //"Pending" => 25,
                1 => 0, //pending
                2=>25,
                3 => 50,
                //"In Transit" => 50,
                //"Out for Delivery" => 75,
                4 => 75,
                //"Delivered" => 100,
                5 => 100,
                //"Failed" or "Returned" => 100,
                6 or 7 or 8 => 100,
                _ => 0
            };
        }

        //implementation of chart CLICK of recent orders
        public async Task<PaginatedList<RecentOrder>> GetRecentOrders(string timeFilter, int ordStatusID, string? barValue, int pageIndex, int pageSize, string dashType, int? customerId)
        {
            var query = ApplyTimeFilterForRecentOrd(_orderDBContext.orderdetails, timeFilter, barValue);

            // Filter by status
            if (ordStatusID == 6 || ordStatusID == 7 || ordStatusID == 8 || ordStatusID == 9)
            {
                var integers = new List<int> { 6, 7, 8, 9 };
                query = query.Where(o => integers.Contains((int)o.order_status_id));
            }
            else
            {
                query = query.Where(o => o.order_status_id == ordStatusID);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync();

            // Apply ordering and pagination
            var items = await query
                .OrderByDescending(o => o.order_status_change_date)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .Select(o => new RecentOrder
                {
                    OrderId = o.order_number ?? "N/A",
                    Customer = o.sender_name ?? "Unknown",
                    Destination = FormatDestination(o.suburb_dropoff, o.state_dropoff),
                    Status = o.order_status_title ?? "Unknown",
                    LastUpdate = o.order_status_change_date.HasValue
                        ? o.order_status_change_date.Value.ToString("MMM dd, hh:mm tt")
                        : "N/A",
                    Progress = GetProgressPercentage((int)o.order_status_id)
                })
                .ToListAsync();

            return new PaginatedList<RecentOrder>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize),
                Items = items
            };
        }

        private IQueryable<orderdetails> ApplyTimeFilterForRecentOrd(IQueryable<orderdetails> query, string timeFilter, string? barValue, string dashType, int? customerId)
        {
            var now = DateTime.Now;
            if (dashType == "Admin")
            {

                query = query.Where(o => o.order_status_change_date != null);
            }
            else if (dashType == "Customer")
            {

                query = query.Where(o => o.order_status_change_date != null && o.courier_id == customerId);//need to verify this logic where is the customer id
            }
 

            if (!string.IsNullOrEmpty(barValue))
            {
                // Handle barValue filtering
                return timeFilter switch
                {
                    "today" => HandleTodayFilter(query, barValue, now),
                    "weekly" => HandleWeeklyFilter(query, barValue, now),
                    "monthly" => HandleMonthlyFilter(query, barValue, now),
                    "yearly" => HandleYearlyFilter(query, barValue, now),
                    _ => query // "overall" or default
                };
            }
            else
            {
                // Original time filtering without barValue
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
        }

        private IQueryable<orderdetails> HandleTodayFilter(IQueryable<orderdetails> query, string barValue, DateTime now)
        {
            // barValue format: "0:00 - 2:00" or similar
            if (barValue.Contains("-") && barValue.Contains(":"))
            {
                var parts = barValue.Split('-');
                if (parts.Length == 2 && int.TryParse(parts[0].Split(':')[0], out int startHour))
                {
                    var endHour = startHour + 2; // Assuming 2-hour intervals
                    return query.Where(o => o.order_status_change_date.Value.Date == now.Date
                                           // &&
                                           //o.order_status_change_date.Value.Hour >= startHour &&
                                           //o.order_status_change_date.Value.Hour < endHour
                                           );
                }
            }
            return query.Where(o => o.order_status_change_date.Value.Date == now.Date);
        }

        private IQueryable<orderdetails> HandleWeeklyFilter(IQueryable<orderdetails> query, string barValue, DateTime now)
        {
            // barValue format: "Mon", "Tue", etc.
            var dayOfWeek = barValue switch
            {
                "Mon" => DayOfWeek.Monday,
                "Tue" => DayOfWeek.Tuesday,
                "Wed" => DayOfWeek.Wednesday,
                "Thu" => DayOfWeek.Thursday,
                "Fri" => DayOfWeek.Friday,
                "Sat" => DayOfWeek.Saturday,
                "Sun" => DayOfWeek.Sunday,
                _ => (DayOfWeek?)null
            };

            if (dayOfWeek.HasValue)
            {
                // Find the most recent occurrence of this day
                var targetDate = now;
                while (targetDate.DayOfWeek != dayOfWeek.Value)
                {
                    targetDate = targetDate.AddDays(-1);
                }

                return query.Where(o => o.order_status_change_date.Value.Date == targetDate.Date);
            }

            return query.Where(o => o.order_status_change_date.Value >= now.AddDays(-7));
        }

        private IQueryable<orderdetails> HandleMonthlyFilter(IQueryable<orderdetails> query, string barValue, DateTime now)
        {
            // barValue format: "5" (day of month)
            if (int.TryParse(barValue, out int dayOfMonth) && dayOfMonth >= 1 && dayOfMonth <= 31)
            {
                return query.Where(o => o.order_status_change_date.Value.Month == now.Month &&
                                      o.order_status_change_date.Value.Year == now.Year &&
                                      o.order_status_change_date.Value.Day == dayOfMonth);
            }

            return query.Where(o => o.order_status_change_date.Value.Month == now.Month &&
                                  o.order_status_change_date.Value.Year == now.Year);
        }

        private IQueryable<orderdetails> HandleYearlyFilter(IQueryable<orderdetails> query, string barValue, DateTime now)
        {
            // barValue format: "Apr", "Mar", etc.
            var monthNames = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            var monthIndex = Array.IndexOf(monthNames, barValue);

            if (monthIndex >= 0)
            {
                return query.Where(o => o.order_status_change_date.Value.Year == now.Year &&
                                      o.order_status_change_date.Value.Month == monthIndex + 1);
            }

            return query.Where(o => o.order_status_change_date.Value.Year == now.Year);
        }

    }
}