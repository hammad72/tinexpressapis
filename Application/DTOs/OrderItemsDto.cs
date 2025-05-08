using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderItemsDto(int? id, string? consignment_number, string? order_number, int? package_type_id, string? package_type,
        int? package_content_id, string? package_content, int? qty, int? weight, double? actual_weight, double? rider_weight,
        int? length, int? width, int? height, string? unit);

    public class ShipmentDetailOrderItemsDTO
    {
        public SenderRecieverOrderItemsDto SenderRecieverOrderItemsDto { get; set; }
        public SummaryOrderItemsDTO SummaryOrderItemsDTO { get; set; }
        public TrackingOrderItemsDTO TrackingOrderItemsDTO { get; set; }
        public ActivityLogOrderItemsDTO ActivityLogOrderItemsDTO { get; set; }
        public PaginatedList<orderitems> paginatedListOrderItems { get; set; }

    }
    public class SenderRecieverOrderItemsDto
    {
        public string? sender_name { get; set; }
        public string? sender_email { get; set; }
        public string? sender_phone { get; set; }
        public string? reciever_name { get; set; }
        public string? reciever_email { get; set; }
        public string? reciever_phone { get; set; }
        public string? address_pickup { get; set; }
        public string? address_dropoff { get; set; }
    }
    public class SummaryOrderItemsDTO
    {
        public float? quote_price { get; set; }
        public float? sale_tax { get; set; }
        public float? price { get; set; }
        public float? net_price { get; set; }
    }
    public class TrackingOrderItemsDTO
    {
        public DateTime? order_status_change_date { get; set; }
    }
    public class ActivityLogOrderItemsDTO
    {
        public DateTime? order_status_change_date { get; set; }
        public int? created_by { get; set; }
        public string username { get; set; }
    }
}
