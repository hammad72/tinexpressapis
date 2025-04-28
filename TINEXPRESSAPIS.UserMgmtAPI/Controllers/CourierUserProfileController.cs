using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourierUserProfileController : ControllerBase
    {
        private readonly ICourierUserProfileService _courierUserProfileService;
        public CourierUserProfileController(ICourierUserProfileService courierUserProfileService)
        {
            _courierUserProfileService = courierUserProfileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _courierUserProfileService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _courierUserProfileService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourierUserProfileDto_Ex dto)
        {
            int ulid = await _courierUserProfileService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCourierUserProfileDto dto)
        {
            await _courierUserProfileService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _courierUserProfileService.DeleteAsync(id);
            return Ok();
        }
    }
}
