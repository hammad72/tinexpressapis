using Application.DTOs;
using Application.Interfaces;
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

        [HttpGet]
        public async Task<ActionResult<DashboardDTO>> GetDashboardData([FromQuery] string timeFilter = "monthly")
        {
            if (!IsValidTimeFilter(timeFilter))
            {
                return BadRequest("Invalid time filter. Valid values are: today, weekly, monthly, yearly, overall");
            }

            var dashboardData = await _dashboardService.GetDashboardData(timeFilter);
            return Ok(dashboardData);
        }

        private bool IsValidTimeFilter(string filter)
        {
            var validFilters = new[] { "today", "weekly", "monthly", "yearly", "overall" };
            return validFilters.Contains(filter.ToLower());
        }
    }
}
