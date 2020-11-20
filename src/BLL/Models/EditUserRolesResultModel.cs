using Microsoft.AspNetCore.Identity;

namespace POC.BLL.Models
{
  public class EditUserRolesResultModel
  {
    public IdentityResult AddResult {get; set;}
    public IdentityResult RemoveResult {get; set;}
  }
}