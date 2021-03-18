using System.Threading.Tasks;
using POC.BLL.Dto;
using Microsoft.AspNetCore.Identity;
using POC.DAL.Entities;

namespace POC.BLL.Services
{
    public interface IEmailConfirmService
    {
        /// <summary>
        /// Send confirmation link to email
        /// </summary>
        /// <param name="user">user data</param>
        /// <returns></returns>
        Task SendConfirmEmailAsync(User user);

        /// <summary>
        /// Method to confirm email
        /// </summary>
        /// <param name="userId"> user ID </param>
        /// <param name="token"> token </param>
        /// <returns></returns>
        Task<IdentityResult> ConfirmEmailAsync(string userId, string token);

        /// <summary>
        /// Check if the users email is confirmed
        /// </summary>
        /// <param name="userDto"> user data </param>
        /// <returns></returns>
        Task<bool> ValidateConfirmedEmailAsync(UserAuthDto userDto);
    }
}
