using System.Threading.Tasks;

namespace POC.DAL.Interfaces
{
  public interface IUnitOfWork
  {
    ICanvasRepository Canvas { get; }
    IOrderRepository Order { get; }
    IFileRepository File { get; }
    Task<int> SaveAsync();
  }
}