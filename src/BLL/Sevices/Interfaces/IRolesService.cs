using System.Collections.Generic;
using System.Threading.Tasks;
using POC.BLL.Models;
using Microsoft.AspNetCore.Identity;
using POC.BLL.DTO;

namespace POC.BLL.Services
{
  public interface IRolesService
  {
    List<IdentityRole> GetRoles();
    Task<IdentityResult> CreateRoleAsync(string name);
    Task<IdentityResult> DeleteRoleAsync(string id);
    Task<IList<string>> GetUserRolesAsync(string userId);
    Task<EditUserRolesResultModel> EditUserRolesAsync(string userId, List<string> roles);
  }
}
