using Application.Interfaces;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TINEXPRESSAPIS.NotificationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailNotificationController : ControllerBase
    {
        public readonly IOTPControlService _otpControlService;
        private readonly IEmailRepository _emailRepository;
        public EmailNotificationController(IEmailRepository emailRepository)
        {
            //_otpControlService = oTPControlService;
            _emailRepository = emailRepository;
            //_userLoginsService = userLoginsService;  
            //_userProfileService = userProfileService;
            //_sendOTPService = sendOTPService;
        }

        [HttpGet("SendOBEmail")]
        public async Task<IActionResult> SendOBEmail(string email, string sName, string pSuburb, string rName, string dSuburb)//, string pType, string pContent)
        {
            var body = $"Hi," +
                $"\n\nYour shipment with the following details is failed from AUTOMATED BOOKING PROCESS due to exceeding budget limit:" +
                $"\n\nSender Name: {sName}" +
                $"\nPickup Suburb: {pSuburb}" +
                $"\nReceiver Name: {rName}" +
                $"\nDrop-off Suburb: {dSuburb}" +
                //$"\nPackage Type: {pType}" +
                //$"\nPackage Content: {pContent}" +
                $"\n\nPlease increase your budget or contact your account manager." +
                $"\n\nRegards," + 
                $"\nTIN Express Support.";

            var result = await _emailRepository.SendEmailAsync(email, "Over-budgeted oder detail", body);
                //email,
                //"Test Email",
                //"Hi,\nYour shipment with following details is failed from AUTOMATED BOOKING PROCESS due to exceeding budget limit:\n" +
                //"\n<b>Sender Name:</b> " + sName+
                //"\nPickup Suburb: " + pSuburb+
                //"\nReciever Name: " + rName+
                //"\nDrop-off Suburb: " + dSuburb+
                //"\nPackage Type: " + pType+
                //"\nPackage Content: " + pContent+
                //"\n\nPlease increase your budget or contact your account manager." +
                //"\n\nRegars," +
                //"\nTIN Express Support."
                //);

            return Ok(new { Success = result });
        }
    }
}
