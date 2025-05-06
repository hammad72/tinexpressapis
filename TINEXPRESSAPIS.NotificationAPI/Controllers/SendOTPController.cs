using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml.Linq;

namespace TINEXPRESSAPIS.NotificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendOTPController : ControllerBase
    {
        //private readonly IUserLoginsService _userLoginsService;

        //public readonly IUserProfileService _userProfileService;
        //public readonly ISendOTPService _sendOTPService;
        public readonly IOTPControlService _otpControlService;
        private readonly IEmailRepository _emailRepository;
        public SendOTPController(IOTPControlService oTPControlService, IEmailRepository emailRepository)
        {
            _otpControlService = oTPControlService;
            _emailRepository = emailRepository;
            //_userLoginsService = userLoginsService;  
            //_userProfileService = userProfileService;
            //_sendOTPService = sendOTPService;
        }
        [HttpGet("send-test-email")]
        public async Task<IActionResult> SendTestEmail()
        {
            var result = await _emailRepository.SendEmailAsync(
                "test@example.com",
                "Test Email",
                "This is a test email from TIN Express");

            return Ok(new { Success = result });
        }
        [HttpPost("SendOTP-admin")]
        public async Task<IActionResult> SendOTPAdmin([FromBody] EmailOTPRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _otpControlService.SendOTPAsync(request.Email,"admin");

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new
            {
                Success = true,
                Message = result.Message,
                uid = result.uid,
                email = result.email
                // OTP = result.OTP // Only include for debugging
            });

        }
        [HttpPost("SendOTP-customer")]
        public async Task<IActionResult> SendOTPCustomer([FromBody] EmailOTPRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _otpControlService.SendOTPAsync(request.Email,"customer");

            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new
            {
                Success = true,
                Message = result.Message,
                uid = result.uid,
                email = result.email
                // OTP = result.OTP // Only include for debugging
            });

        }
        [HttpPost("verify-otp-changePass")]
        public async Task<IActionResult> verifyOtpChangePass([FromBody] OtpChangePassRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _otpControlService.verifyOTpControl(request);


            if (!result.Success)
            {
                return BadRequest(new { result.Message });
            }

            return Ok(new
            {
                Success = true,
                Message = result.Message,
                //uid = result.uid,
                //email = result.email
                // OTP = result.OTP // Only include for debugging
            });

        }


    }

}
