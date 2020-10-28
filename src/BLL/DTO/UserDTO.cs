
using System.Collections.Generic;

namespace POC.BLL.DTO
{
  public class UserDTO
  {
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    
    public UserDTO()
    {
      Roles = new List<string>();
    }
  }
}