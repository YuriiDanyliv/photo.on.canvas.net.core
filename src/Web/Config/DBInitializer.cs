using Microsoft.AspNetCore.Identity;
using POC.DAL.Entities;
using System.Threading.Tasks;

namespace POC.Web.Config
{
  public class DBInitializer
  {
    public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
      string adminEmail = "Yura@mail.com";
      string password = "11111111q";
      var roles = new string[] {"admin", "user"};

      foreach(var role in roles)
      {
        if (await roleManager.FindByNameAsync(role) == null)
        {
          await roleManager.CreateAsync(new IdentityRole(role));
        }
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