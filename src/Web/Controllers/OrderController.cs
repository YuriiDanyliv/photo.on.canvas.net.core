using Microsoft.AspNetCore.Mvc;
using POC.BLL.Dto;
using POC.BLL.Models;
using POC.BLL.Services;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(
          IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get orders and info about pagination
        /// </summary>
        /// <param name="parameters">page number and page size</param>
        /// <returns>Orders and info about pagination</returns>
        [HttpGet("GetOrders")]
        public async Task<ActionResult<PagesListModel<OrderDto>>> GetOrders([FromQuery] OrderQueryParameters parameters)
        {
            var result = await _orderService.GetOrderPagesListAsync(parameters);
            if (result == null || result.data == null || result.data.Length == 0 ) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Create an order
        /// </summary>
        /// <param name="order">Order data</param>
        /// <returns></returns>
        [HttpPost("MakeOrder")]
        public async Task<ActionResult> MakeOrder([FromForm] CreateOrderDto order)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _orderService.MakeOrderAsync(order);

            return Ok();
        }

        /// <summary>
        /// Delete an order
        /// </summary>
        /// <param name="Id">order ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteOrder")]
        public async Task<ActionResult> DeleteOrder([FromQuery] string Id)
        {
            if (Id == null) return BadRequest("No ID");
            await _orderService.DeleteOrderByIdAsync(Id);
            return Ok("Order deleted");
        }
    }
}
