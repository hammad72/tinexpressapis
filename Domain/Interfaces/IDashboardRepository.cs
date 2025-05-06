using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardStats> GetDashboardStats(string timeFilter, string dashType, int? customerId);
        Task<List<OrderStatusDistribution>> GetOrderStatusDistribution(string timeFilter, string dashType, int? customerId);
        Task<List<DailyOrderTrend>> GetDailyOrderTrend(string timeFilter, string dashType, int? customerId);
        Task<PaginatedList<RecentOrder>> GetRecentOrders(string timeFilter,int ordStatusID, string? barValue, int pageIndex, int pageSize, string dashType, int? customerId);
    }
}
