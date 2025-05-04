using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAgingReportRepository
    {
        Task<PaginatedListForAgingReport<AgingReport>> GetAgingReportAsync(
      DateTime? fromDate = null,
      DateTime? toDate = null,
      //IEnumerable<int> excludedStatusIds = null,
      string orderNumber = null,
      int? statusID=null,
      //IEnumerable<int> includedStatusIds = null,
      int pageNumber = 1,
      int pageSize = 10);
    }
}
