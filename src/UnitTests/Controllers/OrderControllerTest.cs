using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.BLL.Services;
using System;
using Web.Controllers;
using Xunit;

namespace UnitTests.Controllers
{
    public class OrderControllerTest
    {
        private readonly Mock<IOrderService> _orderService;
        private readonly OrderController _orderController;
        private readonly IFixture _fixture;

        public OrderControllerTest()
        {
            _orderService = new Mock<IOrderService>();
            _orderController = new OrderController(_orderService.Object);
            _fixture = new Fixture().Customize(new AutoMoqCustomization()); 
        }

        [Fact]
        public async void GetOrders_OrdersExist_ReturnsOkObjectResult()
        {
            //Arrange
            var orders = _fixture.Create<PagesListModel<OrderDto>>();
            var param = new OrderQueryParameters();
            _orderService.Setup(cfg => cfg.GetOrderPagesListAsync(param)).ReturnsAsync(orders);

            //Act
            var response = await _orderController.GetOrders(param);

            //Assert
            Assert.NotNull(response);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var result = Assert.IsType<PagesListModel<OrderDto>>(okResult.Value);
            Assert.Equal(orders, result);
        }

        [Fact]
        public async void GetOrders_OrdersDoNotExist_ReturnsNoContentResult()
        {
            //Arrange
            var orders = new PagesListModel<OrderDto>();
            var param = new OrderQueryParameters();
            _orderService.Setup(cfg => cfg.GetOrderPagesListAsync(param)).ReturnsAsync(orders);

            //Act
            var response = await _orderController.GetOrders(param);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response.Result);
        }

        [Fact]
        public async void CreateOrders_ModelIsValid_ReturnsOkObjectResult()
        {
            //Arrange

            var order = _fixture.Create<CreateOrderDto>();
            _orderService.Setup(cfg => cfg.MakeOrderAsync(order));

            //Act
            var response = await _orderController.MakeOrder(order);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void CreateOrders_ModelIsNotValid_ReturnsBadRequestObjectResult()
        {
            //Arrange
            var order = new CreateOrderDto();
            _orderService.Setup(cfg => cfg.MakeOrderAsync(order));
            _orderController.ModelState.AddModelError("Model", "NotValid");

            //Act
            var response = await _orderController.MakeOrder(order);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Fact]
        public async void DeleteOrders_ValidId_ReturnsOkObjectResult()
        {
            //Arrange
            var id = Guid.NewGuid().ToString();
            _orderService.Setup(cfg => cfg.DeleteOrderByIdAsync(id));

            //Act
            var response = await _orderController.DeleteOrder(id);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
        }

        [Fact]
        public async void DeleteOrders_InvalidId_ReturnsBadRequestObjectResult()
        {
            //Arrange
            _orderService.Setup(cfg => cfg.DeleteOrderByIdAsync(null));

            //Act
            var response = await _orderController.DeleteOrder(null);

            //Assert
            Assert.NotNull(response);
            Assert.IsType<BadRequestObjectResult>(response);
        }

    }
}
