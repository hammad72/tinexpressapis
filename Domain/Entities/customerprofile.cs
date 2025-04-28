using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Net;

namespace Domain.Entities
{
    public class customerprofile
    {
        public int id { get; set; }

        public string name { get; set; }

        public string? legal_name { get; set; }

        public string email { get; set; }

        public string? phone { get; set; }

        public int payment_method_id { get; set; }

        public string? referral_name { get; set; }

        public string? fav_pickup_address { get; set; }

        public string? fav_dropoff_address { get; set; }

        public string? address { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? enrollment_date { get; set; }

        public int? business_type_id { get; set; }

        public string? invoice_frequency { get; set; }

        public string? sales_tax_num { get; set; }

        public bool? proof_of_delivery { get; set; }

        public int? desired_attempts { get; set; }

        public string? other { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? updated_at { get; set; }

        public int? updated_by { get; set; }

        public int status { get; set; }
    }
}