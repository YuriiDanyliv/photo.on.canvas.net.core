using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using POC.DAL.Entities;
using POC.BLL.DTO;
using POC.BLL.Interfaces;
using System.Linq;
using POC.BLL.Mapper;
using AutoMapper.QueryableExtensions;

namespace POC.BLL.Services
{
  public class AccountService : IAccountService
  {
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    public IQueryable<UserDTO> GetUsers()
    {
      var result = _userManager.Users.ProjectTo<UserDTO>(ObjMapper.configuration);

      return result;
    }

    public async Task<IdentityResult> DeleteUserAsync(string userId)
    {
      var user = await _userManager.FindByIdAsync(userId);
      var result = await _userManager.DeleteAsync(user);

      return(result);
    }

    public async Task<IdentityResult> RegisterAsync(UserAuthDTO model)
    {
      User user = new User { Email = model.Email, UserName = model.Email };
      var result = await _userManager.CreateAsync(user, model.Password);
        
      return result;
    }

    public async Task<SignInResult> LoginAsync(UserAuthDTO model)
    {
      var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);

      return result;
    }

    public async Task LogoutAsync()
    {
      await _signInManager.SignOutAsync();
    }
  }
} 