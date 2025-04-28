using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly IUserProfileService _userProfileService;
        public UserProfileController(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> GetAll() => Ok(await _userProfileService.GetAllAsync());

        [HttpGet("Users_Pagna")]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize)
        {
            var paginatedList = await _userProfileService.GetAllAsync(pageIndex, pageSize);
            return Ok(paginatedList);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _userProfileService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserProfileDto_Ex dto)
        {
            int ulid = await _userProfileService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserProfileDto dto)
        {
            await _userProfileService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userProfileService.DeleteAsync(id);
            return Ok();
        }
    }
}
