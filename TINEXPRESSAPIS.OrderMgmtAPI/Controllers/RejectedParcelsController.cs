using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RejectedParcelsController : ControllerBase
    {
        private readonly IRejectedParcelsService _service;
        public RejectedParcelsController(IRejectedParcelsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetAsync(id));

        [HttpGet("GetAllItemsByRPIdAsync")]
        public async Task<IActionResult> GetAllItemsByRPIdAsync(int rpid) => Ok(await _service.GetAllItemsByRPIdAsync(rpid));

        [HttpGet("GetRPwithItemsByRPIdAsync")]
        public async Task<IActionResult> GetRPwithItemsByRPIdAsync(int rpid) => Ok(await _service.GetRPwithItemsByRPIdAsync(rpid));

        [HttpGet("GetByCId")]
        public async Task<IActionResult> GetByCId(int cid) => Ok(await _service.GetByCIdAsync(cid));


        [HttpGet("GetByCId_P")]
        public async Task<IActionResult> GetByCId_P(int pageIndex, int pageSize, /*int? ordSource,*/ int? opt, string? search, int customerID)
        {
            var paginatedList = await _service.GetByCId_P(pageIndex, pageSize,/* ordSource,*/ opt, search, customerID);
            return Ok(paginatedList);
        }



        [HttpPost]
        public async Task<IActionResult> Create(RPDto rpDto)
        {
            if (rpDto == null || rpDto.rp == null || rpDto.rpi == null)
            {
                return BadRequest("Invalid request payload");
            }

            int rpid = await _service.AddAsync(rpDto.rp, rpDto.rpi);
            return Ok(rpid);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _service.DeleteAsync(id);
            return Ok(res);
        }
    }
}
