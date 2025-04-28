using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouriersController : ControllerBase
    {
        private readonly ICouriersService _couriersService;
        public CouriersController(ICouriersService couriersService)
        {
            _couriersService = couriersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _couriersService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _couriersService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCouriersDto dto)
        {
            int ulid = await _couriersService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCouriersDto dto)
        {
            await _couriersService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _couriersService.DeleteAsync(id);
            return Ok();
        }
    }
}
