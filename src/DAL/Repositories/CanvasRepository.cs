using POC.DAL.Context;
using POC.DAL.Entities;
using POC.DAL.Interfaces;

namespace POC.DAL.Repositories
{
  public class CanvasRepository : Repository<Canvas>, ICanvasRepository
  {
    public CanvasRepository(EFContext context) : base(context)
    {
    }
  }
}