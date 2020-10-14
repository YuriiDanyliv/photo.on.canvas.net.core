using POC.DAL.Interfaces;

namespace POC.DAL.Entities
{
  public abstract class BaseEntity : IBaseEntity
  {
    public int Id {get; set; }
  }
}