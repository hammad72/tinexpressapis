using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardDTO> GetDashboardData(string timeFilter)
        {
            var stats = await _dashboardRepository.GetDashboardStats(timeFilter);
            var distribution = await _dashboardRepository.GetOrderStatusDistribution(timeFilter);
            var trend = await _dashboardRepository.GetDailyOrderTrend(timeFilter);
            var recentOrders = await _dashboardRepository.GetRecentOrders(timeFilter);

            return new DashboardDTO
            {
                TotalOrders = stats.TotalOrders,
                Delivered = stats.Delivered,
                InTransit = stats.InTransit,
                Pending = stats.Pending,
                FailedReturned = stats.FailedReturned,
                DeliveredPercentage = stats.DeliveredPercentage,
                InTransitPercentage = stats.InTransitPercentage,
                PendingPercentage = stats.PendingPercentage,
                FailedReturnedPercentage = stats.FailedReturnedPercentage,
                StatusDistribution = distribution.Select(d => new OrderStatusDistributionDTO
                {
                    Status = d.Status,
                    Count = d.Count
                }).ToList(),
                DailyTrend = trend.Select(t => new DailyOrderTrendDTO
                {
                    Day = t.Day,
                    Delivered = t.Delivered,
                    InTransit = t.InTransit,
                    Pending = t.Pending,
                    Failed = t.Failed
                }).ToList(),
                RecentOrders = recentOrders.Select(o => new RecentOrderDTO
                {
                    OrderId = o.OrderId,
                    Customer = o.Customer,
                    Destination = o.Destination,
                    Status = o.Status,
                    LastUpdate = o.LastUpdate,
                    Progress = o.Progress
                }).ToList()
            };
        }
    }
}
