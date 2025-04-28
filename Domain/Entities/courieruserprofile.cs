using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class courieruserprofile
    {
        public int id { get; set; }

        public string first_name { get; set; }

        public string? last_name { get; set; }

        public string? emp_num { get; set; }

        public string email { get; set; }

        public string? dob { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? enrollment_date { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? joining_date { get; set; }

        public string? address { get; set; }

        public string? postal_code { get; set; }
        public int? city_id { get; set; }

        public string? phone_number { get; set; }

        public string? other { get; set; }

        public int courier_id { get; set; }

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
