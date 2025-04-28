using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class usertypes
    {
        public int id { get; set; }
        public string title { get; set; }
        public string? description { get; set; }
        public int? plateform_id { get; set; }
        public string? other { get; set; }
        public int status { get; set; }
    }
}
