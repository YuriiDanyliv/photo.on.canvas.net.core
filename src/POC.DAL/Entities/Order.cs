using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POC.DAL.Entities
{
  [Table("order")]
  public class Order : BaseEntity
  {
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string imgURL { get; set; }

    public Canvas Canvas { get; set; }
    public int CanvasId { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.Now;
  }
}