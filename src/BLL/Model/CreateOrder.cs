using Microsoft.AspNetCore.Http;
using POC.DAL.Entities;

namespace POC.BLL.Model
{
  public class CreateOrder
  {
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string CanvasId { get; set; }
    public IFormFile Image { get; set; }
  }
}