using System.ComponentModel.DataAnnotations.Schema;

namespace POC.DAL.Entities
{
  [Table("canvas")]
  public class Canvas : BaseEntity
  {
    public decimal Price { get; set; }
    public string Size { get; set; }
  }
}