using POC.DAL.Entities;

namespace POC.DAL.Repositories
{
    public class InstagramMediaRepository : Repository<InstagramMedia>, IInstagramMediaRepository
    {
        public InstagramMediaRepository(EFContext context) : base(context)
        {
        }
    }
}