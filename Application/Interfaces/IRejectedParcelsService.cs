using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRejectedParcelsService
    {
        Task<List<RejectedParcelsDto>> GetAllAsync();
        Task<RejectedParcelsDto> GetAsync(int id);
        Task<List<RejectedParcelItemsDto>> GetAllItemsByRPIdAsync(int rpid);
        Task<RPDto> GetRPwithItemsByRPIdAsync(int rpid);
        Task<List<RejectedParcelsDto>> GetByCIdAsync(int cid);
        Task<PaginatedList<RejectedParcelsDto>> GetByCId_P(int pageIndex, int pageSize/*, int? ordSource*/, int? opt, string? search, int? customerID);
        Task<int> AddAsync(RejectedParcelsDto rp, List<RejectedParcelItemsDto> rpi);
        Task<bool> DeleteAsync(int id);
    }
}
