using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
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

        public async Task<DashboardDTO> GetDashboardData(string timeFilter, string dashType, int? customerId)
        {
            var stats = await _dashboardRepository.GetDashboardStats(timeFilter,  dashType, customerId);
            var distribution = await _dashboardRepository.GetOrderStatusDistribution(timeFilter, dashType, customerId);
            var trend = await _dashboardRepository.GetDailyOrderTrend(timeFilter, dashType, customerId);
            //var recentOrders = await _dashboardRepository.GetRecentOrders(timeFilter);

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
                {   id=d.id,
                    Status = d.Status,
                    Count = d.Count
                }).ToList(),
                DailyTrend = trend.Select(t => new DailyOrderTrendDTO
                {
                    Day = t.Day,
                    OrderBooked =t.OrderBooked, 
                    Collected = t.Collected,
                    AwaitingDispatch = t.AwaitingDispatch,
                    OutForDelivery = t.OutForDelivery,
                    Delivered = t.Delivered,
                    Failed = t.Failed
                    //Delivered = t.Delivered,
                    //InTransit = t.InTransit,
                    //Pending = t.Pending,
                    //Failed = t.Failed
                }).ToList(),
                //RecentOrders = recentOrders.Select(o => new RecentOrderDTO
                //{
                //    OrderId = o.OrderId,
                //    Customer = o.Customer,
                //    Destination = o.Destination,
                //    Status = o.Status,
                //    LastUpdate = o.LastUpdate,
                //    Progress = o.Progress
                //}).ToList()
            };
        }


        public async Task<PaginatedList<RecentOrderDTO>> GetRecentOrders(string timeFilter, int ordStatusID, string? barValue, int pageIndex, int pageSize, string dashType, int? customerId)
        {
            var paginatedList = await _dashboardRepository.GetRecentOrders( timeFilter,  ordStatusID, barValue,  pageIndex,  pageSize, dashType, customerId);
            var dtoItems = paginatedList.Items.Select(order => new RecentOrderDTO
            {
                OrderId = order.OrderId,
                Customer = order.Customer,
                Destination = order.Destination,
                Status = order.Status,
                LastUpdate = order.LastUpdate, // or your preferred format
                Progress = order.Progress
            }).ToList();

            var ord = new PaginatedList<RecentOrderDTO>
            {
                PageIndex = paginatedList.PageIndex,
                PageSize = paginatedList.PageSize,
                TotalCount = paginatedList.TotalCount,
                TotalPages = paginatedList.TotalPages,
                Items = dtoItems
            };

            return ord;
        }
    }
}
