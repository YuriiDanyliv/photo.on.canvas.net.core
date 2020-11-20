using System.Threading.Tasks;
using POC.BLL.DTO;
using POC.BLL.Mapper;
using POC.DAL.Entities;
using POC.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace POC.BLL.Services
{
  public class EmailConfirmService : IEmailConfirmService
  {
    private readonly UserManager<User> _userManager;
    private readonly IEmailSenderService _emailSenderService;

    public EmailConfirmService(UserManager<User> userManager, IEmailSenderService emailSenderService)
    {
      _userManager = userManager;
      _emailSenderService = emailSenderService;
    }

    public async Task SendConfirmEmailAsync(UserAuthDTO user, IUrlHelper helper)
    {
      var mappedModel = ObjMapper.Map<UserAuthDTO, User>(user);
      var token = await _userManager.GenerateEmailConfirmationTokenAsync(mappedModel);
    
      var callbackUrl = UrlHelperExtensions.Action(
        helper,
        "ConfirmEmail",
        "Account",
        new { userId = user.Id, token = token }
      );

      EmailMessage message = new EmailMessage(
        new string[] { user.Email },
        "Confirm your email",
        $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>"
      );

      await _emailSenderService.SendEmailAsync(message);
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string userId, string token)
    {
      var user = await _userManager.FindByIdAsync(userId);
      var result = await _userManager.ConfirmEmailAsync(user, token);

      return result;
    }

    public async Task<bool> ValidateConfirmedEmailAsync(UserAuthDTO user)
    {
      var mappedModel = ObjMapper.Map<UserAuthDTO, User>(user);
      var result = await _userManager.IsEmailConfirmedAsync(mappedModel);

      return result;
    }
  }
}