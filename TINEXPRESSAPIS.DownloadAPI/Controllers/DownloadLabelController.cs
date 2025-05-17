using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TINEXPRESSAPIS.DownloadAPI.Helpers;

namespace TINEXPRESSAPIS.DownloadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadLabelController : ControllerBase
    {
        private readonly IDownloadLabelService _service;

        public DownloadLabelController(IDownloadLabelService service)
        {
            _service = service;
        }

        [HttpGet("{refNum}/pdf")]
        public async Task<IActionResult> GetPdf(string refNum)
        {
            try
            {
                var label = await _service.GetLabelAsync(refNum);
                if (label == null)
                    return NotFound();

                var pdfBytes = PdfGenerator.GenerateCourierLabelPdf(label);
                return File(pdfBytes, "application/pdf", $"Label_{refNum}.pdf");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest(ex);
            }
        }
    }
}
