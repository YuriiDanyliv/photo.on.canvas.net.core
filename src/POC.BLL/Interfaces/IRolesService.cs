using System.Collections.Generic;
using System.Threading.Tasks;
using POC.BLL.Model;
using Microsoft.AspNetCore.Identity;

namespace POC.BLL.Interfaces
{
  public interface IRolesService
  {
    List<IdentityRole> GetRoles();
    Task<IdentityResult> CreateRoleAsync(string name);
    Task<IdentityResult> DeleteRoleAsync(string id);
    Task<UserRolesModel> GetUserRolesAsync(string userId);
    Task<EditUserRolesResultModel> EditUserRolesAsync(string userId, List<string> roles);
  }
}
