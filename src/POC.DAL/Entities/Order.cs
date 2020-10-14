using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POC.DAL.Entities
{
  [Table("order")]
  public class Order : BaseEntity
  {
    [Required(ErrorMessage = "Customer name is required")]
    public string CustomerName { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } 

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Canvas is required")]
    public Canvas Canvas { get; set; }

    [Required(ErrorMessage = "Image is required")]
    public string imgURL { get; set; }

    public User User { get; set; }
  }
}