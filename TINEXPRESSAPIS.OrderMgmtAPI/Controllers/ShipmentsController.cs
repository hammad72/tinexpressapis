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
        public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize)
        {
            var paginatedList = await _shipmentService.GetShipmentAsync(pageIndex, pageSize);
            return Ok(paginatedList);
        }
    }
}
