using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class courierapisetting
    {
        [Key]
        [Required]
        public int id { get; set; }

        [Required]
        public int courier_id { get; set; }

        public string? username { get; set; }

        public string? password { get; set; }

        public string? bearer_token { get; set; }

        public int api_type { get; set; }

        public string? api_url { get; set; }
    }
}
