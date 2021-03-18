using Microsoft.AspNetCore.Identity;
using POC.BLL.Models;
using POC.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public class RolesService : IRolesService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> CreateRoleAsync(string name)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(name));
            return result;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> DeleteRoleAsync(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            IdentityResult result = await _roleManager.DeleteAsync(role);

            return result;
        }

        /// <inheritdoc/>
        public List<IdentityRole> GetRoles()
        {
            var roles = _roleManager.Roles.ToList();
            return roles;
        }

        /// <inheritdoc/>
        public async Task<IList<string>> GetUserRolesAsync(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);

            return userRoles;
        }

        /// <inheritdoc/>
        public async Task<EditUserRolesResultModel> EditUserRolesAsync(EditRolesModel model)
        {
            User user = await _userManager.FindByIdAsync(model.UserId);
            var userRoles = await _userManager.GetRolesAsync(user);

            var addResult = await _userManager.AddToRolesAsync(user, model.Roles.Except(userRoles));
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(model.Roles));

            var result = new EditUserRolesResultModel();

            result.Succeeded = false;
            result.Errors.AddRange(addResult.Errors);
            result.Errors.AddRange(removeResult.Errors);

            if (addResult.Succeeded && removeResult.Succeeded) result.Succeeded = true;

            return result;
        }
    }
}
