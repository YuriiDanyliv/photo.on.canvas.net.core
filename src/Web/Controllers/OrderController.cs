using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using POC.BLL.DTO;
using POC.BLL.Model;
using POC.BLL.Interfaces;
using POC.Web.ViewModel;
using System.Linq;
using POC.DAL.Models;
using POC.DAL.Entities;

namespace Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class OrderController : Controller
  {
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrderController(
      ILogger<OrderController> logger,
      IOrderService orderService,
      IMapper mapper)
    {
      _logger = logger;
      _orderService = orderService;
      _mapper = mapper;
    }

    [HttpGet("GetOrders")]
    public ActionResult<PagesList<Order>> GetOrders([FromQuery] OrderParameters parameters)
    {
      var result = _orderService.GetOrderPagesList(parameters);

      return Ok(result);
    }

    [HttpPost("MakeOrder")]
    public async Task<ActionResult> MakeOrder([FromForm] CreateOrder createOrder)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      await _orderService.MakeOrderAsync(createOrder);

      return Ok();
    }

    [HttpPost("DeleteOrder")]
    public async Task<ActionResult> DeleteOrder([FromBody] string Id)
    {
      await _orderService.DeleteOrderByIdAsync(Id);
      return Ok();
    }
  }
}
