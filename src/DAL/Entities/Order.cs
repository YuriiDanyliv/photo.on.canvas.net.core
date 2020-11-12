using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace POC.DAL.Entities
{
  [Table("order")]
  public class Order : BaseEntity
  {
    public string CustomerName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public byte[] Image { get; set; }
    public Canvas Canvas { get; set; }
    public string CanvasId { get; set; }
    public DateTime CreationDate { get; private set; } = DateTime.Now;
  }
}