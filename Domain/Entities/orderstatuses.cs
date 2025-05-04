using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class orderstatuses
    {
        [Key]
        public required int id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public required string order_status { get; set; }
        [Column(TypeName = "varchar(150)")]
        public required string ostatus_courier { get; set; }
        [Column(TypeName = "varchar(150)")]
        public required string ostatus_customer { get; set; }
        [Column(TypeName = "varchar(50)")]
        public required string css_class { get; set; }
        public required int sequence { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? other { get; set; }
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required DateTime created_at { get; set; }
        public required int created_by { get; set; }
        public required int status { get; set; }
    }
}
