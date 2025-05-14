using Application.DTOs;
using Application.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavedPackagesController : ControllerBase
    {
        private readonly ISavedPackagesService _service;
        public SavedPackagesController(ISavedPackagesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        //[HttpGet("{id}")]
        [HttpGet("GetAsyncBySPCode")]
        public async Task<IActionResult> GetAsyncBySPCode(string spCode) => Ok(await _service.GetAsyncBySPCode(spCode));

        [HttpGet("GetByCId")]
        public async Task<IActionResult> GetByCId(int cid) => Ok(await _service.GetByCIdAsync(cid));

        [HttpPost]
        public async Task<IActionResult> Create(List<CreateSavedPackagesDto> dto)
        {
            int faid = await _service.AddAsync(dto);
            return Ok(faid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateSavedPackagesDto dto)
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
