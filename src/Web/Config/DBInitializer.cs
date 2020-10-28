using Microsoft.AspNetCore.Identity;
using POC.DAL.Entities;
using System.Threading.Tasks;

namespace POC.Web.Config
{
  public class DBInitializer
  {
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
      string adminEmail = "Yura@mail.com";
      string password = "11111111q";

      if (await roleManager.FindByNameAsync("admin") == null)
      {
        await roleManager.CreateAsync(new IdentityRole("admin"));
      }
      if (await roleManager.FindByNameAsync("user") == null)
      {
        await roleManager.CreateAsync(new IdentityRole("user"));
      }
      
      if (await userManager.FindByNameAsync(adminEmail) == null)
      {
        User admin = new User { Email = adminEmail, UserName = adminEmail };
        IdentityResult result = await userManager.CreateAsync(admin, password);
        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(admin, "admin");
        }
      }
    }
  }
}