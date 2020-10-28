using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using POC.DAL.Interfaces;

namespace POC.DAL.Entities
{
  public abstract class BaseEntity : IBaseEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public string Id {get; set; }
  }
}