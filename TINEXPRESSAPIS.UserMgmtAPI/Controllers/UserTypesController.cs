using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.UserMgmtAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserTypesController : ControllerBase
    {
        private readonly IUserTypesService _userTypeService;
        public UserTypesController(IUserTypesService userTypeService)
        {
            _userTypeService = userTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _userTypeService.GetAllAsync());

        [HttpGet("{pid}")]
        public async Task<IActionResult> GetUserTypesAllByPlatform(int pid) => Ok(await _userTypeService.GetUserTypesAllByPlatform(pid));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _userTypeService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserTypesDto dto)
        {
            int ulid = await _userTypeService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserTypesDto dto)
        {
            await _userTypeService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}
