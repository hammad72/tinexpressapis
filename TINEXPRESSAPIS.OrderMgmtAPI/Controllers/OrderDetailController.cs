using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController(IOrderDetailsService oService, IGetQuoteService gqService, ICourierBookingService cbService) : ControllerBase
    {
        private readonly IOrderDetailsService _oService = oService;
        private readonly IGetQuoteService _gqService = gqService;
        private readonly ICourierBookingService _cbService = cbService;

        [HttpPost]
        public async Task<IActionResult> Create(OrderDto OrderDto)
        {
            // comment
            string cn = await _oService.AddAsync(OrderDto.odd, OrderDto.oid);
            return Ok(cn);
        }

        [HttpPost("GetQuoteZU")]
        public async Task<IActionResult> GetQuoteZU(object data)
        {
            // comment
            string cn = await _gqService.getQuoteZoom2u(data);
            return Ok(cn);
        }

        [HttpPost("GetQuoteCP")]
        public async Task<IActionResult> GetQuoteCP(object data)
        {
            // comment
            string cn = await _gqService.getQuoteCourierPlease(data);
            return Ok(cn);
        }

        [HttpPost("OrderBookingZU")]
        public async Task<IActionResult> OrderBookingZU(object data)
        {
            // comment
            string cn = await _cbService.OrderBookingZU(data);
            return Ok(cn);
        }

        [HttpPost("OrderBookingCP")]
        public async Task<IActionResult> OrderBookingCP(object data)
        {
            // comment
            string cn = await _cbService.OrderBookingCP(data);
            return Ok(cn);
        }
    }
}
