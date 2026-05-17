using System;
using System.Collections.Generic;
using System.Text;

namespace CodePulse.Application.DTOs.Authentification
{
    public class LoginResponseDto
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
    }
}
