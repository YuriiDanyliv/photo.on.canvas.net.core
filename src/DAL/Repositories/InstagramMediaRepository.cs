using POC.DAL.Context;
using POC.DAL.Entities;
using POC.DAL.Interfaces;

namespace POC.DAL.Repositories
{
  public class InstagramMediaRepository : Repository<InstagramMedia>, IInstagramMediaRepository
  {
    public InstagramMediaRepository(EFContext context) : base(context)
    {
    }
  }
}