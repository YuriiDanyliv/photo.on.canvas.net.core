using System.Threading.Tasks;
using POC.DAL.Context;
using POC.DAL.Interfaces;

namespace POC.DAL.Repositories
{
  public class UnitOfWork : IUnitOfWork
  {
    private EFContext _context;

    private ICanvasRepository _canvas;
    private IOrderRepository _order;
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

    public async Task<int> SaveAsync()
    {
      var result = await _context.SaveChangesAsync();
      return result;
    }
  }
}