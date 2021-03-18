using BLL.Mapper;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.DAL.Entities;
using POC.DAL.Models;
using POC.DAL.Repositories;
using System.Threading.Tasks;

namespace POC.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc/>
        public async Task DeleteOrderByIdAsync(string Id)
        {
            var order = await _unitOfWork.Order.FindByIdAsync(Id);
            _unitOfWork.Order.Delete(order);
            await _unitOfWork.SaveAsync();
        }

        /// <inheritdoc/>
        public async Task<PagesListModel<OrderDto>> GetOrderPagesListAsync(OrderQueryParameters parameters)
        {
            var ordersList = await _unitOfWork.Order.GetByQueryParamAsync(parameters);
            var result = ObjMapper.Map<PagesList<Order>, PagesListModel<OrderDto>>(ordersList);

            return result;
        }

        /// <inheritdoc/>
        public async Task MakeOrderAsync(CreateOrderDto orderDto)
        {
            var order = ObjMapper.Map<CreateOrderDto, Order>(orderDto);
            //order.Canvas = await _unitOfWork.Canvas.FindByIdAsync(orderDto.CanvasId);

            _unitOfWork.Order.Create(order);
            await _unitOfWork.SaveAsync();
        }
    }
}