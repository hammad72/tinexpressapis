using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class orderitems
    {
        [Key]
        public int id { get; set; }
        public string? consignment_number { get; set; }
        public string? order_number { get; set; }
        public int? package_type_id { get; set; }
        public string? package_type { get; set; }
        public int? package_content_id { get; set; }
        public string? package_content { get; set; }
        public int? qty { get; set; }
        public int? weight { get; set; }
        public double? actual_weight { get; set; }
        public double? rider_weight { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string? unit { get; set; }

    }
}
