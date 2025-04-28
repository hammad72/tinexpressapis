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
}
