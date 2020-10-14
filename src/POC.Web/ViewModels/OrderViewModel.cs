using System.ComponentModel.DataAnnotations;

namespace POC.Web.ViewModel
{
  public class OrderViewModel
  {
    [Required(ErrorMessage = "Необхідно вказати Імя покупця")]
    public string CustomerName { get; set; }

    [Required(ErrorMessage = "Необхідно вказати контактний номер телефону")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } 

    public string Address { get; set; }

    public string imgURL { get; set; }

    [Required(ErrorMessage = "Необхідно вказати тип полотна")]
    public CanvasViewModel Canvas { get; set; }
  }
}