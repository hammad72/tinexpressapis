using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private readonly IUserLoginsService _userLoginsService;
        public UserLoginsController(IUserLoginsService userLoginsService)
        {
            _userLoginsService = userLoginsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userLoginsService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _userLoginsService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserLoginsDto dto)
        {
            int ulid = await _userLoginsService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserLoginsDto dto)
        {
            await _userLoginsService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userLoginsService.DeleteAsync(id);
            return Ok();
        }
    }
}
