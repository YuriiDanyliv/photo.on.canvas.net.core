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

    public PagesList<Order> GetOrderPagesList(OrderParameters parameters)
    {
      return _unitOfWork.Order.GetByQueryParam(parameters);
    }

    public async Task MakeOrderAsync(OrderDTO order)
    {
      var mappedModel = ObjMapper.Map<OrderDTO, Order>(order);
      _unitOfWork.Order.Create(mappedModel);
      await _unitOfWork.SaveAsync();
    }
  }
}