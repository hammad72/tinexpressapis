using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
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
        public async Task<IActionResult> Create(OrderDto dto)
        {
            string cn = await _oService.AddAsync(dto.odd, dto.oid);
            return Ok(cn);
        }
    }
}
