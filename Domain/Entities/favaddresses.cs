using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class favaddresses
    {
        public int id { get; set; }
        public string type { get; set; }
        public string address { get; set; }
        public string suburb { get; set; }
        public string postcode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string latlong { get; set; }
        public int customer_id { get; set; }
        public int status { get; set; }
    }
}
