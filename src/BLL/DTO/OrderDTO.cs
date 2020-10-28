
namespace POC.BLL.DTO
{
  public class OrderDTO
  {
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; } 
    public string Address { get; set; }
    public string imgURL { get; set; }
    public CanvasDTO Canvas { get; set; }
    public string CanvasId { get; set; }
  }
}