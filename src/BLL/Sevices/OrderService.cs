using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    private readonly IFileService _fileService;

    public OrderService(IUnitOfWork unitOfWork, IFileService fileService)
    {
      _unitOfWork = unitOfWork;
      _fileService = fileService;
    }

    public async Task DeleteOrderByIdAsync(string Id)
    {
      var order = await _unitOfWork.Order.FindByIdAsync(Id);
      _unitOfWork.Order.Delete(order);
      await _unitOfWork.SaveAsync();
    }

    public PagesList<Order> GetOrderPagesList(OrderParameters parameters)
    {
      return _unitOfWork.Order.GetByQueryParam(parameters);
    }

    public async Task MakeOrderAsync(CreateOrder order)
    {
      var canvas = await _unitOfWork.Canvas.FindByIdAsync(order.CanvasId);
      var image =  await _fileService.AddOrderedImage(order.Image);

      if (canvas.Equals(null) || image.Equals(null))
      {
        throw new System.ArgumentNullException($"Error, canvas is {canvas}, image is {image}");
      } 

      var res = _unitOfWork.Order.Create(
        new Order {
          CustomerName = order.CustomerName,
          Address = order.Address,
          PhoneNumber = order.PhoneNumber,
          Canvas = canvas,
          Image = image,
        }
      );
      await _unitOfWork.SaveAsync();
    }
  }
}