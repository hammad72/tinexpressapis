using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class orderdetails
    {
        [Key] 
        public string? consignment_number { get; set; }
        public string? order_number { get; set; }
        public string? sender_name { get; set; }
        public string? sender_email { get; set; }
        public string? sender_phone { get; set; }
        public string? reciever_name { get; set; }
        public string? reciever_email { get; set; }
        public string? reciever_phone { get; set; }
        public string? address_pickup { get; set; }
        public string? unit_num_pickup { get; set; }
        public string? street_number_pickup { get; set; }
        public string? street_pickup { get; set; }
        public string? suburb_pickup { get; set; }
        public string? postcode_pickup { get; set; }
        public string? state_pickup { get; set; }
        public string? country_pickup { get; set; }
        public bool? building_type_pickup { get; set; }
        public bool? tail_lift_pickup { get; set; }
        public string? address_dropoff { get; set; }
        public string? unit_num_dropoff { get; set; }
        public string? street_number_dropoff { get; set; }
        public string? street_dropoff { get; set; }
        public string? suburb_dropoff { get; set; }
        public string? postcode_dropoff { get; set; }
        public string? state_dropoff { get; set; }
        public string? country_dropoff { get; set; }
        public bool? building_type_dropoff { get; set; }
        public bool? tail_lift_dropoff { get; set; }
        public bool? pobox_dropoff { get; set; }
        public int? courier_id { get; set; }
        public string? courier_title { get; set; }
        public string? delivery_type { get; set; }
        public DateTime? booking_date { get; set; }
        public DateTime? collection_datetime { get; set; }
        public string? pickup_date { get; set; }
        public string? pickup_time { get; set; }
        public DateTime? assigned_date { get; set; }
        public DateTime? delivered_datetime { get; set; }
        public float? quote_price { get; set; }
        public float? sale_tax { get; set; }
        public float? price { get; set; }
        public float? net_price { get; set; }
        public bool? leave_at_delivery { get; set; }
        public string? special_instructions { get; set; }
        public string? order_latlong { get; set; }
        public int? order_status_id { get; set; }
        public string? order_status_title { get; set; }
        public DateTime? order_status_change_date { get; set; }
        public string? status_latlong { get; set; }
        public string? status_css_class { get; set; }
        public int? order_status_id_prev { get; set; }
        public string? order_status_prev_title { get; set; }
        public DateTime? order_status_change_date_prev { get; set; }
        public string? status_latlong_prev { get; set; }
        public string? status_css_class_prev { get; set; }
        public int? no_of_attempts { get; set; }
        public DateTime? last_attempt_at { get; set; }
        public int? failed_attempts { get; set; }
        public string? reason { get; set; }
        public string? reason_customer { get; set; }
        public string? reason_courier { get; set; }
        public int? failed_attempts_return { get; set; }
        public string? reason_return { get; set; }
        public string? reason_lost { get; set; }
        public string? received_by { get; set; }
        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? created_at { get; set; }
        public int? created_by { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? updated_at { get; set; }
        public int? updated_by { get; set; }
        public string? updated_by_name { get; set; }
        public int? warehouse_id { get; set; }
        public string? warehouse_title { get; set; }
        public string? rider_name { get; set; }
        public string? payment_status { get; set; }
        public string? invoice_number { get; set; }
        public DateTime? invoice_date { get; set; }
        public string? tracking_link { get; set; }
        public string? tracking_code { get; set; }
    }
    public class order
    {
        public orderdetails odd { get; set; }
        public List<orderitems> oid { get; set; }
    }
    public class shipment_columns {
        public string? consignment_number { get; set; }
        public string? order_number { get; set; }
      
        public string? reciever_name { get; set; }
        public string? order_status_title { get; set; }
        public DateTime? order_status_change_date { get; set; }
        public string? suburb_dropoff { get; set; }
    }
    public class AgingReport
    {
        public string ConsignmentNumber { get; set; }
        public string OrderNumber { get; set; }
        public string SenderName { get; set; }
        public string OrderStatusTitle { get; set; }
        public int DaysInStatus { get; set; }
        public string AgingBucket { get; set; }
        public DateTime OrderStatusChangeDate { get; set; }
        public string SuburbDropoff { get; set; }
        public string StateDropoff { get; set; }
        public DateTime? CollectedDate { get; set; }
    }
}
