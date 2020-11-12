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
    public ActionResult<PagesVM<OrderResponseVM>> GetOrders([FromQuery] OrderParameters parameters)
    {
      var result = _mapper.Map<PagesVM<OrderResponseVM>>(_orderService.GetOrderPagesList(parameters));

      return Ok(result);
    }

    [HttpPost("MakeOrder")]
    public async Task<ActionResult> MakeOrder([FromForm] CreateOrderVM createOrder)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);
      await _orderService.MakeOrderAsync(_mapper.Map<CreateOrder>(createOrder));

      return Ok();
    }

    [HttpDelete("DeleteOrder")]
    public async Task<ActionResult> DeleteOrder([FromQuery] string Id)
    {
      await _orderService.DeleteOrderByIdAsync(Id);
      return Ok();
    }
  }
}
