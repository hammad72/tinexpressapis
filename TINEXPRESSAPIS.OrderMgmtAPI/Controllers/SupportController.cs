using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.OrderMgmtAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SupportController : ControllerBase
    {

        private readonly ISupportService _supportService;
        private readonly string _fileUploadPath;

        public SupportController(ISupportService supportService, IConfiguration configuration)
        {
            //_supportService = supportService;
            //_fileUploadPath = configuration["FileUploadPath"] ?? Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

            //if (!Directory.Exists(_fileUploadPath))
            //{
            //    Directory.CreateDirectory(_fileUploadPath);
            //}
            _supportService = supportService;
            _fileUploadPath = configuration["FileUploadPath"]
                              ?? Path.Combine(Directory.GetCurrentDirectory());

            if (!Directory.Exists(_fileUploadPath))
            {
                Console.WriteLine("No Directory Found");
                //Directory.CreateDirectory(_fileUploadPath);
            }
        }

        [HttpPost("add-support-complain")]
        [RequestSizeLimit(50_000_000)] // 50MB
        public async Task<IActionResult> CreateSupportRequest([FromForm] SupportDto supportDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _supportService.CreateSupportRequestAsync(supportDto, _fileUploadPath);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync(int pageIndex, int pageSize, int? cid)
        {
            var paginatedList = await _supportService.getALL(pageIndex, pageSize, cid);
            return Ok(paginatedList);
        }
        [HttpGet("preview")]
        public IActionResult PreviewFile([FromQuery] string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return BadRequest("Filename is required");

            var safeFilename = Path.GetFileName(filename); // prevent directory traversal
            var folderPath = Path.Combine("C:\\inetpub\\AllProjectMedia\\supportcomplainfiles");
            var filePath = Path.Combine(folderPath, safeFilename);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var mimeType = "application/pdf"; // or use content-type detection logic
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Return the file with no 'attachment' header so the browser previews it
            return File(stream, mimeType);
        }

        [HttpGet("download")]
        public IActionResult DownloadFile([FromQuery] string filename)
        {
            if (string.IsNullOrWhiteSpace(filename))
                return BadRequest("Filename is required");

            // Prevent directory traversal
            var safeFilename = Path.GetFileName(filename);

            var folderPath = Path.Combine("C:\\inetpub\\AllProjectMedia\\supportcomplainfiles");
            var filePath = Path.Combine(folderPath, safeFilename);

            if (!System.IO.File.Exists(filePath))
                return NotFound("File not found");

            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            var mimeType = "application/pdf"; // Optional: detect based on extension

            return File(stream, mimeType, safeFilename);
        }
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] StatusUpdateDto dto)
        {
            if (dto == null || dto.Id <= 0)
                return BadRequest("Invalid request");

            var rep = await _supportService.updateStatus(dto.Id, dto.Status, dto.Userid);
            if (rep == 1)
            {
                return Ok(new {rep=1, message = "Status updated successfully" });

            }
            else
            {
                return Ok(new {rep=0, message = "Status updated successfully" });

            }
        }

    }
}

