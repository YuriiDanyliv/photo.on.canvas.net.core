using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using POC.DAL.Entities;

namespace POC.BLL.Models
{
  public class LoginResult
  {
    public SignInResult SignInResult {get; set; }
    public string Jwt { get; set; }
  }
}