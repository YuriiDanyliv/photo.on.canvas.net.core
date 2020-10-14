using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using POC.BLL.Interfaces;
using POC.BLL.Model;
using POC.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace POC.BLL.Services
{
  public class RolesService : IRolesService
  {
    RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<User> _userManager;

    public RolesService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
    {
      _roleManager = roleManager;
      _userManager = userManager;
    }

    public async Task<IdentityResult> CreateRoleAsync(string name)
    {
      var result = await _roleManager.CreateAsync(new IdentityRole(name));
      return result;
    }

    public async Task<IdentityResult> DeleteRoleAsync(string id)
    {
      IdentityRole role = await _roleManager.FindByIdAsync(id);
      IdentityResult result = await _roleManager.DeleteAsync(role);

      return result;
    }

    public List<IdentityRole> GetRoles()
    {
      var result = _roleManager.Roles.ToList();
      return result;
    }

    public async Task<UserRolesModel> GetUserRolesAsync(string userId)
    {
      User user = await _userManager.FindByIdAsync(userId);
      var userRoles = await _userManager.GetRolesAsync(user);
      var allRoles = _roleManager.Roles.ToList();

      UserRolesModel model = new UserRolesModel
      {
        UserId = user.Id,
        UserEmail = user.Email,
        UserRoles = userRoles,
        AllRoles = allRoles
      };

      return model;
    }

    public async Task<EditUserRolesResultModel> EditUserRolesAsync(string userId, List<string> roles)
    {
      User user = await _userManager.FindByIdAsync(userId);
      var userRoles = await _userManager.GetRolesAsync(user);

      var addResult = await _userManager.AddToRolesAsync(user, roles.Except(userRoles));
      var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(roles));

      return new EditUserRolesResultModel {AddResult = addResult, RemoveResult = removeResult};
    }
  }
}
