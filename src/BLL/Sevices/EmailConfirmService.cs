using System.Threading.Tasks;
using POC.BLL.Dto;
using POC.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POC.BLL.Models;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using BLL.Mapper;

namespace POC.BLL.Services
{
    public class EmailConfirmService : IEmailConfirmService
    {
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSenderService _emailSenderService;

        public EmailConfirmService(
            IActionContextAccessor actionContextAccessor,
            UserManager<User> userManager,
            IEmailSenderService emailSenderService)
        {
            _actionContextAccessor = actionContextAccessor;
            _userManager = userManager;
            _emailSenderService = emailSenderService;
        }

        /// <inheritdoc/>
        public async Task SendConfirmEmailAsync(User user)
        {
            var url = new UrlHelper(_actionContextAccessor.ActionContext);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = url.Action(
              "ConfirmEmail",
              "Account",
              new { userId = user.Id, token }
            );

            var message = new EmailMessage(
              new string[] { user.Email },
              "Confirm your email",
              $"<a href='{callbackUrl}'>link</a>"
            );

            await _emailSenderService.SendEmailAsync(message);
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.ConfirmEmailAsync(user, token);

            return result;
        }

        /// <inheritdoc/>
        public async Task<bool> ValidateConfirmedEmailAsync(UserAuthDto userDto)
        {
            var user = ObjMapper.Map<UserAuthDto, User>(userDto);
            var result = await _userManager.IsEmailConfirmedAsync(user);

            return result;
        }
    }
}