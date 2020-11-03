using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

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
    public IFormFile Image { get; set; }
  }
}
  
