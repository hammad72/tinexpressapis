using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerProfileController : ControllerBase
    {
        private readonly ICustomerProfileService _customerService;
        public CustomerProfileController(ICustomerProfileService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _customerService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _customerService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerProfileDto_Ex dto)
        {
            int ulid = await _customerService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerProfileDto dto)
        {
            await _customerService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerService.DeleteAsync(id);
            return Ok();
        }
    }
}
