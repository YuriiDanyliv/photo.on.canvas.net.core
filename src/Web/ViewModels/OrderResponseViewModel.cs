using System.ComponentModel.DataAnnotations;

namespace POC.Web.ViewModel
{
  public class OrderResponseViewModel
  {
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; } 
    public string Address { get; set; }
    public string imgURL { get; set; }
    public CanvasViewModel Canvas { get; set; }
  }
}