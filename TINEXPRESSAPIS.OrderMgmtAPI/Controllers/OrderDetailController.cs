using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailsService _oService;
        public OrderDetailController(IOrderDetailsService oService)
        {
            _oService = oService;
        }
        [HttpPost]
        public async Task<IActionResult> Create(OrderDto OrderDto)
        {
            string cn = await _oService.AddAsync(OrderDto.odd, OrderDto.oid);
            return Ok(cn);
        }
    }
}
