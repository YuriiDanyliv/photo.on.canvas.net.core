using System.ComponentModel.DataAnnotations;
using POC.DAL.Entities;

namespace POC.Web.ViewModel
{
  public class OrderResponseVM
  {
    public string Id { get; set; }
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; } 
    public string Address { get; set; }
    public CanvasResponseVM Canvas { get; set; }
  }
}