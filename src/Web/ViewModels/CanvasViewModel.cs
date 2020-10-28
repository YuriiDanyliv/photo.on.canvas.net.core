using System.ComponentModel.DataAnnotations;

namespace POC.Web.ViewModel
{
  public class CanvasViewModel
  {
    public string Id {get; private set;}

    [Required(ErrorMessage = "Необхідно вказати ціну полотна")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Необхідно вказати розмір полотна")]
    public string Size { get; set; }

    public string Name {get; private set;}
  }
}