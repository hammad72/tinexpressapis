using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Interfaces
{
    public interface ISupportRepository
    {
        Task<supportcomplains> CreateSupportRequestAsync(SupportEnityRef supportDto, List<string> filePaths);
        Task<PaginatedList<supportcomplains>> getAll(int pageIndex, int pageSize, int? cid);
        Task<int> updateStatus(int id, int status, int userid);
    }
}
