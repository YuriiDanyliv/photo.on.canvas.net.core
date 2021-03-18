using POC.BLL.Dto;
using POC.BLL.Models;
using POC.DAL.Entities;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public interface IOrderService
    {
        /// <summary>
        /// Get all orders with pagination info
        /// </summary>
        /// <param name="parameters">page size and page number</param>
        /// <returns></returns>
        Task<PagesListModel<OrderDto>> GetOrderPagesListAsync(OrderQueryParameters parameters);

        /// <summary>
        /// Create new order
        /// </summary>
        /// <param name="order">order data</param>
        /// <returns></returns>
        Task MakeOrderAsync(CreateOrderDto order);

        /// <summary>
        /// Delete order by ID
        /// </summary>
        /// <param name="Id">order id</param>
        /// <returns></returns>
        Task DeleteOrderByIdAsync(string Id);
    }
}