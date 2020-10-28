using System.Threading.Tasks;
using POC.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace POC.BLL.Interfaces
{
  public interface IEmailConfirmService
  {
    Task SendConfirmEmailAsync(UserAuthDTO user, IUrlHelper helper);
    Task<IdentityResult> ConfirmEmailAsync(string userId, string token);
    Task<bool> ValidateConfirmedEmailAsync(UserAuthDTO user);
  }
}
