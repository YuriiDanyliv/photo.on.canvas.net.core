using POC.DAL.Entities;

namespace POC.DAL.Repositories
{
    public class CanvasRepository : Repository<Canvas>, ICanvasRepository
    {
        public CanvasRepository(EFContext context) : base(context)
        {
        }
    }
}