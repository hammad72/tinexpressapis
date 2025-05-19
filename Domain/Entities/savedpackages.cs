using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class savedpackages
    {
        [Key]
        public int id { get; set; }
        public string? sp_code { get; set; }
        public int? package_type_id { get; set; }
        public string? package_type { get; set; }
        public int? package_content_id { get; set; }
        public string? package_content { get; set; }
        public int? qty { get; set; }
        public int? weight { get; set; }
        public int? length { get; set; }
        public int? width { get; set; }
        public int? height { get; set; }
        public string? unit { get; set; }
        public int? customer_id { get; set; }
    }
}
