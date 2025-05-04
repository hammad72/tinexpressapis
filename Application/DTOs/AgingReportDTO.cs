using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class AgingReportDTO
    {
        public string DateRange { get; set; } // "3days", "week", "month", "year", "custom"
        //public DateTime? CustomFromDate { get; set; }
        //public DateTime? CustomToDate { get; set; }
        //public List<int> StatusIds { get; set; }
        public int? statusID { get; set; }
        public string OrderNumber { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
