using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class DashboardEntities
    {
    }
    public class DashboardStats
    {
        public int TotalOrders { get; set; }
        public int Delivered { get; set; }
        public int InTransit { get; set; }
        public int Pending { get; set; }
        public int FailedReturned { get; set; }
        public decimal DeliveredPercentage { get; set; }
        public decimal InTransitPercentage { get; set; }
        public decimal PendingPercentage { get; set; }
        public decimal FailedReturnedPercentage { get; set; }
    }
    public class OrderStatusDistribution
    {
        public string Status { get; set; }
        public int Count { get; set; }
    }
    public class DailyOrderTrend
    {
        public string Day { get; set; }
        public int Delivered { get; set; }
        public int InTransit { get; set; }
        public int Pending { get; set; }
        public int Failed { get; set; }
    }
    public class RecentOrder
    {
        public string OrderId { get; set; }
        public string Customer { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public string LastUpdate { get; set; }
        public int Progress { get; set; } // 0-100%
    }
}
