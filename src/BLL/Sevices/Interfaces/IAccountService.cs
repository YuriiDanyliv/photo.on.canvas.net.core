using Microsoft.AspNetCore.Identity;
using POC.BLL.Dto;
using POC.BLL.Models;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Returns list with users and info about pagination
        /// </summary>
        /// <param name="param">Page number and page size </param>
        /// <returns>pagination model</returns>
        Task<PagesListModel<UserDto>> GetUsersAsync(UserQueryParam param);

        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <returns></returns>
        Task<IdentityResult> DeleteUserAsync(string userId);

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="userDto"></param>
        /// <param name="Url"></param>
        /// <returns></returns>
        Task<IdentityResult> RegisterAsync(UserAuthDto userDto);

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns>login result</returns>
        Task<LoginResult> LoginAsync(UserAuthDto model);

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        Task LogoutAsync();
    }
}
