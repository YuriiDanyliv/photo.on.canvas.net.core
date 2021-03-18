using System.Collections.Generic;

namespace POC.BLL.Models
{
    public class EditRolesModel
    {
        public string UserId { get; set; }

        public List<string> Roles { get; set; }
    }
}