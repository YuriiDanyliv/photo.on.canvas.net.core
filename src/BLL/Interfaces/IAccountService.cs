using System.Linq;
using System.Threading.Tasks;
using POC.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using POC.DAL.Models;
using POC.BLL.Model;

namespace POC.BLL.Interfaces
{
  public interface IAccountService
  {
    PagesList<UserDTO> GetUsers(UserQueryParam param);
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IdentityResult> RegisterAsync(UserAuthDTO model);
    Task<SignInResult> LoginAsync(UserAuthDTO model);
    Task LogoutAsync();

  }
}
