using System.Threading.Tasks;
using POC.BLL.DTO;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Models;

namespace POC.BLL.Services
{
  public interface IOrderService
  {
    PagesList<Order> GetOrderPagesList(OrderParameters parameters);
    Task MakeOrderAsync(CreateOrder createOrder);
    Task DeleteOrderByIdAsync(string Id);
  }
}