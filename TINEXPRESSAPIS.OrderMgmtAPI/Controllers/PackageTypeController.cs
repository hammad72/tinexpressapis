using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageTypeController : ControllerBase
    {
        private readonly IPackageTypeService _packageTypeService;
        public PackageTypeController(IPackageTypeService packageTypeService)
        {
            _packageTypeService = packageTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _packageTypeService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _packageTypeService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreatePackageTypeDto dto)
        {
            int ulid = await _packageTypeService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePackageTypeDto dto)
        {
            await _packageTypeService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _packageTypeService.DeleteAsync(id);
            return Ok();
        }
    }
}
