using System.Threading.Tasks;

namespace POC.DAL.Repositories
{
    public interface IUnitOfWork
    {
        ICanvasRepository Canvas { get; }
        IOrderRepository Order { get; }
        IInstagramMediaRepository InstaMedia { get; }
        IFileRepository File { get; }
        Task<int> SaveAsync();
    }
}