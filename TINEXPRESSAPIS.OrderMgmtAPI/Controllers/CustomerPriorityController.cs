using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerPriorityController : ControllerBase
    {
        private readonly ICustomerPriorityService _service;
        public CustomerPriorityController(ICustomerPriorityService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        //[HttpGet("{id}")]
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        //[HttpGet("{cid}")]
        [HttpGet("GetByCId")]
        public async Task<IActionResult> GetByCId(int cid) => Ok(await _service.GetByCIdAsync(cid));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerPriorityDtoArr dto)
        {
            bool res = await _service.AddAsync(dto.customer_priorities);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerPriorityDto dto)
        {
            await _service.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }
    }
}
