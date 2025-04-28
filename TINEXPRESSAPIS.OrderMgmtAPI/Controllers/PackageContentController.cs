using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageContentController : ControllerBase
    {
        private readonly IPackageContentService _packageContentService;
        public PackageContentController(IPackageContentService packageContentService)
        {
            _packageContentService = packageContentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _packageContentService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _packageContentService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreatePackageContentDto dto)
        {
            int ulid = await _packageContentService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePackageContentDto dto)
        {
            await _packageContentService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _packageContentService.DeleteAsync(id);
            return Ok();
        }
    }
}
