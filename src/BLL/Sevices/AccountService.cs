using BLL.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRolesService _rolesService;
        private readonly IConfiguration _config;
        private readonly IEmailConfirmService _emailConfirmService;
        private readonly IConfigurationService<EmailServiceConfig> _configService;

        public AccountService(
          UserManager<User> userManager,
          SignInManager<User> signInManager,
          IRolesService rolesService,
          IConfiguration config,
          IEmailConfirmService emailConfirmService,
          IConfigurationService<EmailServiceConfig> configService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _rolesService = rolesService;
            _config = config;
            _emailConfirmService = emailConfirmService;
            _configService = configService;
        }

        /// <inheritdoc/>
        public async Task<PagesListModel<UserDto>> GetUsersAsync(UserQueryParam param)
        {
            var users = ObjMapper.Map<IList<User>, IList<UserDto>>(await _userManager.Users.ToListAsync());
            var usersPagesList = await PagesList<UserDto>.GetPagesListAsync(users.AsQueryable(), param);
            
            foreach (var user in usersPagesList)
            {
                user.Roles = await _rolesService.GetUserRolesAsync(user.Id);
            }

            var result = ObjMapper.Map<PagesList<UserDto>, PagesListModel<UserDto>>(usersPagesList);
            return result;
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user);

            return (result);
        }

        /// <inheritdoc/>
        public async Task<IdentityResult> RegisterAsync(UserAuthDto userDto)
        {
            var result = await _userManager.CreateAsync(
                new User { Email = userDto.Email, UserName = userDto.Email }, 
                userDto.Password);

            if (!result.Succeeded) return result;

            var emailConfig = await _configService.GetSettingsAsync();
            if (emailConfig.EmailConfirmIsOn)
            {
                var user = _userManager.Users.SingleOrDefault(user => user.Email == userDto.Email);
                await _emailConfirmService.SendConfirmEmailAsync(user);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task<LoginResult> LoginAsync(UserAuthDto model)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            var result = new LoginResult();
            result.SignInResult = signInResult;

            if (signInResult.Succeeded)
            {
                var user = _userManager.Users.SingleOrDefault(user => user.Email == model.Email);
                var roles = await _rolesService.GetUserRolesAsync(user.Id);
                result.Jwt = GenerateJwtToken(user, roles);
            }

            return result;
        }

        /// <inheritdoc/>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        /// <inheritdoc/>
        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var cfg = _config.GetSection("JWT");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg.GetValue<string>("Key")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(cfg.GetValue<string>("ExpireDays")));

            var token = new JwtSecurityToken(
              cfg.GetValue<string>("Issuer"),
              cfg.GetValue<string>("Audience"),
              claims,
              expires: expires,
              signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}