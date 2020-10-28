using System.ComponentModel.DataAnnotations;
using POC.DAL.Interfaces;

namespace POC.DAL.Entities
{
  public abstract class BaseEntity : IBaseEntity
  {
    [Key]
    public int Id {get; set; }
  }
}