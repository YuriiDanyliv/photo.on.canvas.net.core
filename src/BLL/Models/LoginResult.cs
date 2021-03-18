using Microsoft.AspNetCore.Identity;

namespace POC.BLL.Models
{
    public class LoginResult
    {
        public SignInResult SignInResult { get; set; }

        public string Jwt { get; set; }
    }
}