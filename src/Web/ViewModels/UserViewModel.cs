using System.Collections.Generic;

namespace POC.Web.ViewModel
{
  public class UserViewModel
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
  }
}
