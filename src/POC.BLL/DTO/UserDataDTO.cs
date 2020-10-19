using System.Collections.Generic;

namespace POC.BLL.DTO
{
  public class UserDataDTO
  {
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public UserDataDTO()
    {
      Roles = new List<string>();
    }
  }
}
