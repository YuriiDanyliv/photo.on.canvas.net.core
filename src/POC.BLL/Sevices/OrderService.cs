using System.Threading.Tasks;
using POC.BLL.DTO;
using POC.BLL.Interfaces;
using POC.BLL.Mapper;
using POC.BLL.Model;
using POC.DAL.Entities;
using POC.DAL.Interfaces;
using POC.DAL.Models;

namespace POC.BLL.Services
{
  public class OrderService : IOrderService
  {
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
      _unitOfWork = unitOfWork;
    }

    public async Task DeleteOrderByIdAsync(int Id)
    {
      var order = await _unitOfWork.Order.FindByIdAsync(Id);
      _unitOfWork.Order.Delete(order);
      await _unitOfWork.SaveAsync();
    }

    public PagesList<Order> GetOrderPagesList(OrderParameters parameters)
    {
      return _unitOfWork.Order.GetByQueryParam(parameters);
    }

    public async Task MakeOrderAsync(OrderDTO orderDTO)
    {
      var order = ObjMapper.Map<OrderDTO, Order>(orderDTO);
      order.Canvas = await _unitOfWork.Canvas.FindByIdAsync(orderDTO.CanvasId);
      _unitOfWork.Order.Create(order);
      await _unitOfWork.SaveAsync();
    }
  }
}