using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace POC.DAL.Entities
{
  [Table("user")]
  public class User : IdentityUser
  {
   
  }
}