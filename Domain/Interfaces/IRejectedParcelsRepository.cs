using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRejectedParcelsRepository
    {
        Task<List<rejectedparcels>> GetAllAsync();
        Task<rejectedparcels> GetAsync(int id);
        Task<List<rejectedparcelitems>> GetAllItemsByRPIdAsync(int rpid);
        Task<rp_rpi> GetRPwithItemsByRPIdAsync(int rpid);
        Task<List<rejectedparcels>> GetByCIdAsync(int cid);
        Task<PaginatedList<rejectedparcels>> GetByCId_P(int pageIndex, int pageSize,/* int? ordSource,*/ int? opt, string? search, int? customerID);
        Task<int> AddAsync(rejectedparcels rp, List<rejectedparcelitems> rpi);
        Task<bool> DeleteAsync(int id);
    }
}
