using System.Linq;
using System.Threading.Tasks;
using POC.BLL.DTO;
using POC.BLL.Interfaces;
using POC.BLL.Mapper;
using POC.DAL.Entities;
using POC.DAL.Interfaces;

namespace POC.BLL.Services
{
  public class CanvasService : ICanvasService
  {
    private readonly IUnitOfWork _unitOfWork;

    public CanvasService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task CreateCanvasAsync(CanvasDTO model)
    {
      var mappedModel = ObjMapper.Map<CanvasDTO, Canvas>(model);
      _unitOfWork.Canvas.Create(mappedModel);
      await _unitOfWork.SaveAsync();
    }

    public async Task DeleteCanvasByIdAsync(int Id)
    {
      var canvas = await _unitOfWork.Canvas.FindByIdAsync(Id);
      _unitOfWork.Canvas.Delete(canvas);
      await _unitOfWork.SaveAsync();
    }

    public IQueryable<Canvas> GetCanvas()
    {
      var result = _unitOfWork.Canvas.FindAll();
      return result;
    }
  }
}