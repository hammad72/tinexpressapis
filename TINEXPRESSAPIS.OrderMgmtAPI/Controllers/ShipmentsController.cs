using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentsController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;
        public ShipmentsController(IShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }
        [HttpGet("get-ordersource")]
        public async Task<IActionResult> GetAllOrderSourceAsync() => Ok(await _shipmentService.GetAllOrderSourceAsync());

        [HttpGet("get-options")]
        public async Task<IActionResult> GetAllOptionsAsync() => Ok(await _shipmentService.GetAllOptionsAsync());

        [HttpGet("get-shipments")]
        public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search)
        {
            var paginatedList = await _shipmentService.GetShipmentAsync(pageIndex, pageSize, ordSource, opt, search,null);
            return Ok(paginatedList);
        }
        [HttpGet("export/csv")]
        public async Task<IActionResult> ExportCsv(int? ordSource, int? opt, string? search)
        {
            var fileContent = await _shipmentService.ExportShipmentsToCsv(ordSource, opt, search,null);
            return File(fileContent, "text/csv", $"shipments_{DateTime.Now:yyyyMMdd}.csv");
        }

        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportExcel(int? ordSource, int? opt, string? search)
        {
            var fileContent = await _shipmentService.ExportShipmentsToExcel(ordSource, opt, search,null);
            return File(fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"shipments_{DateTime.Now:yyyyMMdd}.xlsx");
        }
        [HttpGet("get-shipments-customer")]
        public async Task<IActionResult> GetAllCustomerAsync(int pageIndex, int pageSize, int? ordSource, int? opt, string? search,int customerID)
        {
            var paginatedList = await _shipmentService.GetShipmentAsync(pageIndex, pageSize, ordSource, opt, search,customerID);
            return Ok(paginatedList);
        }
        [HttpGet("export-customer/csv")]
        public async Task<IActionResult> ExportCustomerCsv(int? ordSource, int? opt, string? search, int? customerID)
        {
            var fileContent = await _shipmentService.ExportShipmentsToCsv(ordSource, opt, search,customerID);
            return File(fileContent, "text/csv", $"shipments_{DateTime.Now:yyyyMMdd}.csv");
        }

        [HttpGet("export-customer/excel")]
        public async Task<IActionResult> ExportCustomerExcel(int? ordSource, int? opt, string? search, int? customerID)
        {
            var fileContent = await _shipmentService.ExportShipmentsToExcel(ordSource, opt, search,customerID);
            return File(fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"shipments_{DateTime.Now:yyyyMMdd}.xlsx");
        }
    }
}
