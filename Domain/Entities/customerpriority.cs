using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class customerpriority
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public int courier_id { get; set; }
        public string priority { get; set; }
    }
    public class customerpriorityArr
    {
        public List<customerpriority> customer_priorities { get; set; }
    }
}
