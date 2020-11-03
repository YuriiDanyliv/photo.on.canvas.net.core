using System.Linq;
using System.Threading.Tasks;
using POC.BLL.DTO;
using POC.DAL.Entities;

namespace POC.BLL.Interfaces
{
  public interface ICanvasService
  {
    Task CreateCanvasAsync(Canvas canvas);
    Task DeleteCanvasByIdAsync(string Id);
    IQueryable<Canvas> GetCanvas();
  }
}
