using System.Linq;
using System.Threading.Tasks;
using POC.BLL.DTO;
using Microsoft.AspNetCore.Identity;

namespace POC.BLL.Interfaces
{
  public interface IAccountService
  {
    IQueryable<UserDTO> GetUsers();
    Task<IdentityResult> DeleteUserAsync(string userId);
    Task<IdentityResult> RegisterAsync(UserAuthDTO model);
    Task<SignInResult> LoginAsync(UserAuthDTO model);
    Task LogoutAsync();

  }
}
