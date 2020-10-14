using System.ComponentModel.DataAnnotations;

namespace POC.Web.ViewModel
{
  public class CanvasViewModel
  {
    [Required(ErrorMessage = "Необхідно вказати ціну полотна")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Необхідно вказати розмір полотна")]
    public string Size { get; set; }
  }
}