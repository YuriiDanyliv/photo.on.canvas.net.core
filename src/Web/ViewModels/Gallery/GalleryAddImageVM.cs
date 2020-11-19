using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace POC.Web.ViewModel
{
  public class GalleryAddImageVM
  {
    [Required(ErrorMessage = "Необхідно завантажити зображення")]
    public IFormFile Image { get; set; }
  }
}