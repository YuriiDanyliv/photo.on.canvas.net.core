using System.ComponentModel.DataAnnotations;

namespace POC.Web.ViewModel
{
  public class LoginViewModel
  {
    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Пароль")]
    public string Password { get; set; }
  }
}