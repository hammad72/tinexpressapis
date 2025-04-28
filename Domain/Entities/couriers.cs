using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class couriers
    {
        public int id { get; set; }

        public string name { get; set; }

        public string? legal_name { get; set; }

        public string? primary_location { get; set; }

        public int payment_method_id { get; set; }

        public string? restricted_item { get; set; }

        public string email { get; set; }

        public string? phone { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? enrollment_date { get; set; }

        public string? far_away_distance { get; set; }

        public string? postal_code { get; set; }

        public int? city_id { get; set; }

        public int? state_id { get; set; }

        public int? country_id { get; set; }

        public string? web_site { get; set; }

        public string? logo { get; set; }

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
