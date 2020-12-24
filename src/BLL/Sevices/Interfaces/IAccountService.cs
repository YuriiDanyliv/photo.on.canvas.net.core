using System.Linq;
using System.Threading.Tasks;
using POC.BLL.DTO;
using Microsoft.AspNetCore.Identity;
using POC.DAL.Models;
using POC.BLL.Models;

namespace POC.BLL.Services
{
  public interface IAccountService
  {
    PagesList<UserDTO> GetUsers(UserQueryParam param);
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IdentityResult> RegisterAsync(UserAuthDTO model);
    Task<LoginResult> LoginAsync(UserAuthDTO model);
    Task LogoutAsync();

  }
}
