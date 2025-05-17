using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ISupportService
    {
        Task<SupportResponseDto> CreateSupportRequestAsync(SupportDto supportDto, string fileUploadPath);
        Task<PaginatedList<supportcomplainsDTO>> getALL(int pageIndex, int pageSize,int? cid);
        Task<int> updateStatus(int id, int status, int userid);
    }
}
