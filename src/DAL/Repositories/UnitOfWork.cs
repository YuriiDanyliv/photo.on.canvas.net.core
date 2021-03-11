using System.Threading.Tasks;

namespace POC.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context;

        private ICanvasRepository _canvas;
        private IOrderRepository _order;
        private IInstagramMediaRepository _instaMedia;
        private IFileRepository _file;

        public UnitOfWork(EFContext context)
        {
            _context = context;
        }

        public ICanvasRepository Canvas
        {
            get => _canvas ??= new CanvasRepository(_context);
        }

        public IOrderRepository Order
        {
            get => _order ??= new OrderRepository(_context);
        }

        public IFileRepository File
        {
            get => _file ??= new FileRepository(_context);
        }

        public IInstagramMediaRepository InstaMedia
        {
            get => _instaMedia ??= new InstagramMediaRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }
    }
}