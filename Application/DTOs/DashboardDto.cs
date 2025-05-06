using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DashboardDTO
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
        public List<OrderStatusDistributionDTO> StatusDistribution { get; set; }
        public List<DailyOrderTrendDTO> DailyTrend { get; set; }
        //public List<RecentOrderDTO> RecentOrders { get; set; }
    }

    public class OrderStatusDistributionDTO
    {
        public int id { get; set; }
        public string Status { get; set; }
        public int Count { get; set; }
    }

    public class DailyOrderTrendDTO
    {
        public string Day { get; set; }
        public int OrderBooked { get; set; }
        public int Collected { get; set; }
        public int AwaitingDispatch { get; set; }
        public int OutForDelivery { get; set; }
        public int Delivered { get; set; }
        public int Failed { get; set; }

        //public int Delivered { get; set; }
        //public int InTransit { get; set; }
        //public int Pending { get; set; }
        //public int Failed { get; set; }
    }

    public class RecentOrderDTO
    {
        public string OrderId { get; set; }
        public string Customer { get; set; }
        public string Destination { get; set; }
        public string Status { get; set; }
        public string LastUpdate { get; set; }
        public int Progress { get; set; }
    }
}
