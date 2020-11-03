using System.ComponentModel.DataAnnotations;

namespace POC.Web.ViewModel
{
  public class CanvasResponseVM
  {
    public string Id {get; private set;}
    public decimal Price { get; set; }
    public string Size { get; set; }
    public string Name {get; private set;}
  }
}