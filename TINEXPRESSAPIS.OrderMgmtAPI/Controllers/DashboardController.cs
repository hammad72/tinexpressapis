using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]//Admin Dashboard
        public async Task<ActionResult<DashboardDTO>> GetDashboardData([FromQuery] string timeFilter = "monthly")
        {
            if (!IsValidTimeFilter(timeFilter))
            {//test GIT
                return BadRequest("Invalid time filter. Valid values are: today, weekly, monthly, yearly, overall");
            }

            var dashboardData = await _dashboardService.GetDashboardData(timeFilter,"Admin",null);
            return Ok(dashboardData);
        }
        [HttpGet("get-recent-orders")] //admin
        public async Task<IActionResult> getRecentOrders(string timeFilter, int ordStatusID,string? barValue, int pageIndex, int pageSize)
        {
            var paginatedList = await _dashboardService.GetRecentOrders( timeFilter,  ordStatusID,  barValue, pageIndex, pageSize, "Admin", null);
            return Ok(paginatedList);
        }

        private bool IsValidTimeFilter(string filter)
        {
            var validFilters = new[] { "today", "weekly", "monthly", "yearly", "overall" };
            return validFilters.Contains(filter.ToLower());
        }

        [HttpGet("for-customer")] //Dashboard For Customer
        public async Task<ActionResult<DashboardDTO>> GetDashboardDataCustomer([FromQuery] int customerID, [FromQuery] string timeFilter = "monthly" )
        {
            if (!IsValidTimeFilter(timeFilter))
            {//test GIT
                return BadRequest("Invalid time filter. Valid values are: today, weekly, monthly, yearly, overall");
            }

            var dashboardData = await _dashboardService.GetDashboardData(timeFilter, "Customer",customerID);
            return Ok(dashboardData);
        }
        [HttpGet("get-recent-orders-customer")] 
        public async Task<IActionResult> getRecentOrders(string timeFilter, int ordStatusID, string? barValue, int pageIndex, int pageSize,int customerID)
        {
            var paginatedList = await _dashboardService.GetRecentOrders(timeFilter, ordStatusID, barValue, pageIndex, pageSize, "Customer", customerID);
            return Ok(paginatedList);
        }
    }
}
