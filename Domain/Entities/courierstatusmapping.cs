using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class courierstatusmapping
    {
        [Key]
        public required int id { get; set; }
        public required int courier_id { get; set; }
        public required int courier_status_id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public required string courier_status_title { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string? description { get; set; }
        public required int tin_status_id { get; set; }
        public required int status { get; set; }
    }
}
