using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class userotp
    {
        [Key]
        public int id { get; set; }
        public int user_id { get; set; }
        public string otp { get; set; }

        public string email { get; set; }

        [Required]
        [Column(TypeName = "TIMESTAMP")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime created_at { get; set; }

        public int created_by { get; set; }

    }
}
