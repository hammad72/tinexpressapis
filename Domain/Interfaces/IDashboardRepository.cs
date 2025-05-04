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
        Task<DashboardStats> GetDashboardStats(string timeFilter);
        Task<List<OrderStatusDistribution>> GetOrderStatusDistribution(string timeFilter);
        Task<List<DailyOrderTrend>> GetDailyOrderTrend(string timeFilter);
        Task<List<RecentOrder>> GetRecentOrders(string timeFilter);
    }
}
