using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerBudgetController : ControllerBase
    {
        private readonly ICustomerBudgetService _service;
        public CustomerBudgetController(ICustomerBudgetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());


        [HttpGet("GetById")]
        //[HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

        //[HttpGet("{cid}")]
        [HttpGet("GetByCId")]
        public async Task<IActionResult> GetByCId(int cid) => Ok(await _service.GetByCIdAsync(cid));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerBudgetDtoArr dto)
        {
            bool res = await _service.AddAsync(dto.customer_budgets);
            return Ok(res);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerBudgetDto dto)
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
