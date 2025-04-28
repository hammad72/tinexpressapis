using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerUserProfileController : ControllerBase
    {
        private readonly ICustomerUserProfileService _customerUserProfileService;
        public CustomerUserProfileController(ICustomerUserProfileService customerUserProfileService)
        {
            _customerUserProfileService = customerUserProfileService;
        }


        [HttpGet("Users")]
        public async Task<IActionResult> GetAll() => Ok(await _customerUserProfileService.GetAllAsync());

        [HttpGet("Users_Pagna")]
        public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize)
        {
            var paginatedList = await _customerUserProfileService.GetAllAsync(pageIndex, pageSize);
            return Ok(paginatedList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _customerUserProfileService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerUserProfileDto_Ex dto)
        {
            int ulid = await _customerUserProfileService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerUserProfileDto dto)
        {
            await _customerUserProfileService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _customerUserProfileService.DeleteAsync(id);
            return Ok();
        }
    }
}
