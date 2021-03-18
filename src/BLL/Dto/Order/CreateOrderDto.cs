using Microsoft.AspNetCore.Http;
using POC.BLL.Attributes;
using System.ComponentModel.DataAnnotations;

namespace POC.BLL.Dto
{
    public class CreateOrderDto
    {
        [Required(ErrorMessage = "It is necessary to specify the customer name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the delivery address")]
        public string DeliveryAddress { get; set; }

        [Required(ErrorMessage = "It is necessary to specify the canvas size")]
        public string CanvasId { get; set; }

        [Required(ErrorMessage = "Image must be uploaded")]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; }
    }
}