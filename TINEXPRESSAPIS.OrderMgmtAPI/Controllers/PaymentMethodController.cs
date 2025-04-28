using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _paymentMethodService.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id) => Ok(await _paymentMethodService.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreatePaymentMethodDto dto)
        {
            int ulid = await _paymentMethodService.AddAsync(dto);
            return Ok(ulid);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdatePaymentMethodDto dto)
        {
            await _paymentMethodService.UpdateAsync(dto);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _paymentMethodService.DeleteAsync(id);
            return Ok();
        }
    }
}