using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using POC.DAL.Entities;
using POC.BLL.DTO;
using POC.BLL.Mapper;
using AutoMapper.QueryableExtensions;
using POC.DAL.Models;
using POC.BLL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace POC.BLL.Services
{
  public class AccountService : IAccountService
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IRolesService _rolesService;
    private readonly IConfiguration _config;

    public AccountService(
      UserManager<User> userManager,
      SignInManager<User> signInManager,
      IRolesService rolesService,
      IConfiguration config)
    {
      _userManager = userManager;
      _signInManager = signInManager;
      _rolesService = rolesService;
      _config = config;
    }

    public PagesList<UserDTO> GetUsers(UserQueryParam param)
    {
      var users = _userManager.Users.ProjectTo<UserDTO>(ObjMapper.configuration);
      var usersList = PagesList<UserDTO>.GetPagesList(users, param.PageNumber, param.PageSize);

      foreach (var user in usersList)
      {
        user.Roles = _rolesService.GetUserRolesAsync(user.Id).Result;
      }

      return usersList;
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      var result = await _userManager.DeleteAsync(user);

      return (result);
    }

    public async Task<IdentityResult> RegisterAsync(UserAuthDTO model)
    {
      User user = new User { Email = model.Email, UserName = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);

      return result;
    }

    public async Task<LoginResult> LoginAsync(UserAuthDTO model)
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

    public async Task LogoutAsync()
    {
      await _signInManager.SignOutAsync();
    }

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