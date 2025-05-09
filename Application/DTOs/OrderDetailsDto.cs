using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderDetailsDto(string? consignment_number,string? order_number,string? sender_name,string? sender_email,
        string? sender_phone,string? reciever_name,string? reciever_email,string? reciever_phone,string? address_pickup,
        string? unit_num_pickup,string? street_number_pickup,string? street_pickup,string? suburb_pickup,string? postcode_pickup, string? city_pickup,
        string? state_pickup,string? country_pickup,bool? building_type_pickup,bool? tail_lift_pickup,string? address_dropoff,
        string? unit_num_dropoff,string? street_number_dropoff,string? street_dropoff,string? suburb_dropoff,string? postcode_dropoff, string? city_dropoff,
        string? state_dropoff,string? country_dropoff,bool? building_type_dropoff,bool? tail_lift_dropoff,bool? pobox_dropoff,
        int? courier_id,string? courier_title,string? delivery_type,DateTime? booking_date,DateTime? collection_datetime,
        string? pickup_date,string? pickup_time,DateTime? assigned_date,DateTime? deliverd_datetime,float? quote_price,
        float? sale_tax,float? price,float? net_price,bool? leave_at_delivery,string? special_instructions,string? order_latlong,
        int? order_status_id,string? order_status_title,DateTime? order_status_change_date,string? status_latlong,
        string? status_css_class,int? order_status_id_prev,string? order_status_prev_title,DateTime? order_status_change_date_prev,
        string? status_latlong_prev,string? status_css_class_prev,int? no_of_attempts,DateTime? last_attempt_at,int? failed_attempts,
        string? reason,string? reason_customer,string? reason_courier,int? failed_attempts_return,string? reason_return,
        string? reason_lost,string? received_by,DateTime? created_at,int? created_by,DateTime? updated_at,int? updated_by,
        string? updated_by_name,int? warehouse_id,string? warehouse_title,string? rider_name,string? payment_status,string? invoice_number,
        DateTime? invoice_date,string? tracking_link,string? tracking_code,int? customer_id,string? customer_title);
    public record CreateOrderDetailsDto(string? order_number, string? sender_name, string? sender_email,
        string? sender_phone, string? reciever_name, string? reciever_email, string? reciever_phone, string? address_pickup,
        string? unit_num_pickup, string? street_number_pickup, string? street_pickup, string? suburb_pickup, string? postcode_pickup, string? city_pickup,
        string? state_pickup, string? country_pickup, bool? building_type_pickup, bool? tail_lift_pickup, string? address_dropoff,
        string? unit_num_dropoff, string? street_number_dropoff, string? street_dropoff, string? suburb_dropoff, string? postcode_dropoff, string? city_dropoff,
        string? state_dropoff, string? country_dropoff, bool? building_type_dropoff, bool? tail_lift_dropoff, bool? pobox_dropoff,
        int? courier_id, string? courier_title, string? delivery_type, DateTime? booking_date, DateTime? collection_datetime,
        string? pickup_date, string? pickup_time, float? quote_price, float? sale_tax, float? price, float? net_price, bool? leave_at_delivery,
        string? special_instructions, string? order_latlong,DateTime? order_status_change_date, string? status_latlong,
        int? no_of_attempts, int? created_by, string? tracking_link, string? tracking_code, int? customer_id, string? customer_title);
}
