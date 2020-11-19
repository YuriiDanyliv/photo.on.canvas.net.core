using System.Collections.Generic;

namespace POC.Web.ViewModel
{
  public class GalleryGetImageVM
  {
    public string Id { get; set; }
    public string ImageName { get; set; }
    public string ContentType { get; set; }
    public byte[] Image { get; set; }
  }
}