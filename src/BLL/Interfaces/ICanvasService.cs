using System.Linq;
using System.Threading.Tasks;
using POC.BLL.DTO;

namespace POC.BLL.Interfaces
{
  public interface ICanvasService
  {
    Task CreateCanvasAsync(CanvasDTO canvasDTO);
    Task DeleteCanvasByIdAsync(int Id);
    IQueryable<CanvasDTO> GetCanvas();
  }
}
