using Microsoft.AspNetCore.Identity;
using POC.BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public interface IRolesService
    {
        /// <summary>
        /// Get all roles
        /// </summary>
        /// <returns>list of roles</returns>
        List<IdentityRole> GetRoles();

        /// <summary>
        /// Create new role
        /// </summary>
        /// <param name="name">role name</param>
        /// <returns></returns>
        Task<IdentityResult> CreateRoleAsync(string name);

        /// <summary>
        /// Delete role by ID
        /// </summary>
        /// <param name="id">role id</param>
        /// <returns></returns>
        Task<IdentityResult> DeleteRoleAsync(string id);

        /// <summary>
        /// Get all user roles by user ID
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns></returns>
        Task<IList<string>> GetUserRolesAsync(string userId);

        /// <summary>
        /// Edit user roles
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<EditUserRolesResultModel> EditUserRolesAsync(EditRolesModel model);
    }
}
