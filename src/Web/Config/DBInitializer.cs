using Microsoft.AspNetCore.Identity;
using POC.BLL.Interfaces;
using POC.DAL.Entities;
using System.Threading.Tasks;

namespace POC.Web.Config
{
  public class DBInitializer
  {
    public static async Task InitializeAsync(
      UserManager<User> userManager, 
      RoleManager<IdentityRole> roleManager,
      ICanvasService canvasService)
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

      var canvases = new Canvas []
      {
        new Canvas {Price = 121, Size = "20x30"},
        new Canvas {Price = 188, Size = "35x45"},
        new Canvas {Price = 255, Size = "40x60"},
        new Canvas {Price = 350, Size = "50x75"},
        new Canvas {Price = 418, Size = "60x80"},
        new Canvas {Price = 514, Size = "70x90"},
      };

      foreach (var canvas in canvases)
      {
        await canvasService.CreateCanvasAsync(canvas);  
      }
    }
  }
}