using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using POC.Web.Helpers.Attributes;

namespace POC.Web.ViewModel
{
  public class GalleryAddImageVM
  {
    [Required(ErrorMessage = "Необхідно завантажити зображення")]
    [MaxFileSize(10 * 1024 * 1024)]
    [AllowedExtensions(new string[] {".jpg", ".png"})]
    public IFormFile Image { get; set; }
  }
}