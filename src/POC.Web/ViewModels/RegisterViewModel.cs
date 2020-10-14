using System.ComponentModel.DataAnnotations;
 
namespace POC.Web.ViewModel
{
    public class RegisterViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required]
        [Compare("Password", ErrorMessage = "Паролі не ідентичні")]
        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        public string PasswordConfirm { get; set; }
    }
}