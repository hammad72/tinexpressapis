using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class SocialLoginDto
    {
        public string Provider { get; set; } // "Google" or "Facebook"
        public string Token { get; set; }     // ID Token (Google) or Access Token (Facebook)
    }
}
