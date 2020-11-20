using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using POC.Web.Helpers.Attributes;

namespace POC.Web.ViewModel
{
  public class CreateOrderVM
  {
    [Required(ErrorMessage = "Необхідно вказати імя замовника")]
    public string CustomerName { get; set; }

    [Required(ErrorMessage = "Необхідно вказати номер телефону")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Необхідно вказати адрес доставки")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Необхідно вказати тип полотна")]
    public string CanvasId { get; set; }

    [Required(ErrorMessage = "Необхідно завантажити зображення")]
    [MaxFileSize(10 * 1024 * 1024)]
    [AllowedExtensions(new string[] {".jpg", ".png"})]
    public IFormFile Image { get; set; }
  }
}
  
