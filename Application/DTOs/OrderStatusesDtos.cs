using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderStatusesDto(int id, string order_status, string ostatus_courier, string ostatus_customer,
        string css_class, int sequence, string? other, DateTime created_at, int created_by, int status);
    public record CreateOrderStatusesDto(string order_status, string ostatus_courier, string ostatus_customer,
        string css_class, int sequence, string? other, DateTime created_at, int created_by, int status);
    public record UpdateOrderStatusesDto(int id, string order_status, string ostatus_courier, string ostatus_customer,
        string css_class, int sequence, string? other, DateTime created_at, int created_by, int status);
}
