using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{//new commit
   
        [Route("api/[controller]")]
        [ApiController]
        public class ReportsController : ControllerBase
        {
            private readonly IAgingReportService _agingReportService;

            public ReportsController(IAgingReportService agingReportService)
            {
                _agingReportService = agingReportService;
            }

            [HttpGet("aging")]
            public async Task<ActionResult<PaginatedListForAgingReport<AgingReportDTO>>> GetAgingReport(
                [FromQuery] string dateRange = null,//"3days", "week", "month", "year"
                //[FromQuery] DateTime? customFromDate = null,
                //[FromQuery] DateTime? customToDate = null,
                //[FromQuery] List<int> statusIds = null,
                [FromQuery] int? statusID=null,
                [FromQuery] string orderNumber = null,
                [FromQuery] int pageNumber = 1,
                [FromQuery] int pageSize = 10)
            {
                var filters = new AgingReportDTO
                {
                    DateRange = dateRange,
                    //CustomFromDate = customFromDate,
                    //CustomToDate = customToDate,
                    statusID = statusID,
                    OrderNumber = orderNumber,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };

                var result = await _agingReportService.GetAgingReportAsync(filters);
                return Ok(result);
            }

            //[HttpGet("aging/export")]
            //public async Task<IActionResult> ExportAgingReport(
            //    [FromQuery] string dateRange = null,
            //    [FromQuery] DateTime? customFromDate = null,
            //    [FromQuery] DateTime? customToDate = null,
            //    [FromQuery] List<int> statusIds = null,
            //    [FromQuery] string orderNumber = null,
            //    [FromQuery] string format = "excel")
            //{
            //    var filters = new AgingReportDTO
            //    {
            //        DateRange = dateRange,
            //        CustomFromDate = customFromDate,
            //        CustomToDate = customToDate,
            //        StatusIds = statusIds,
            //        OrderNumber = orderNumber
            //    };

            //    var result = await _agingReportService.GetAgingReportAsync(filters);

            //    return format.ToLower() switch
            //    {
            //        "excel" => ExportToExcel(result.Items),
            //        "csv" => ExportToCsv(result.Items),
            //        _ => BadRequest("Invalid export format. Supported formats: excel, csv")
            //    };
            //}

            //private IActionResult ExportToExcel(IEnumerable<AgingReport> reports)
            //{
            //    // Implement Excel export using EPPlus or similar
            //    // Return FileResult with Excel content
            //    return Ok("Excel export would be implemented here");
            //}

            //private IActionResult ExportToCsv(IEnumerable<AgingReport> reports)
            //{
            //    // Implement CSV export
            //    // Return FileResult with CSV content
            //    return Ok("CSV export would be implemented here");
            //}
        }
    }

