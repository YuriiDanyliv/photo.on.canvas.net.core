using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POC.DAL.Entities
{
  [Table("canvas")]
  public class Canvas : BaseEntity
  {
    [Required(ErrorMessage = "Price is required")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Size is required")]
    [DataType(DataType.Password)]
    public string Size { get; set; }
  }
}