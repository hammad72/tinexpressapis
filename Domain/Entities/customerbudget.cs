using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class customerbudget
    {
        public int id { get; set; }
        public int customer_id { get; set; }
        public required string destination { get; set; }
        public float budget { get; set; }
    }
    public class customerbudgetArr
    {
        public List<customerbudget> customer_budgets { get; set; }
    }
}
