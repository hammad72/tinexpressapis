using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardData(string timeFilter, string dashType, int? customerId);
        //Task<List<RecentOrder>> GetRecentOrders(string timeFilter, int ordStatusID);
       Task <PaginatedList<RecentOrderDTO>> GetRecentOrders(string timeFilter, int ordStatusID, string? barValue, int pageIndex, int pageSize, string dashType, int? customerId);
    }
}
