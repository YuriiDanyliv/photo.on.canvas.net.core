using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace POC.BLL.Models
{
    public class EditUserRolesResultModel
    {
        public List<IdentityError> Errors { get; set; }

        public bool Succeeded { get; set; }

        public EditUserRolesResultModel()
        {
            Errors = new List<IdentityError>();
        }
    }
}