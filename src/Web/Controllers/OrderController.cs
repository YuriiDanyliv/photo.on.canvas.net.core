using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using POC.BLL.DTO;
using POC.BLL.Model;
using POC.BLL.Interfaces;
using POC.Web.ViewModel;
using System.Linq;

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

    [HttpPost("GetOrders")]
    public ActionResult<OrderResponseViewModel> GetOrders([FromBody] OrderParameters parameters)
    {
      var result = _orderService.GetOrderPagesList(parameters)
      .Select(x => new OrderResponseViewModel() 
      { 
        Id = x.Id.ToString(),
        CustomerName = x.CustomerName,
        Address = x.Address,
        PhoneNumber = x.PhoneNumber,
        imgURL = x.imgURL,
        Canvas = _mapper.Map<CanvasViewModel>(x.Canvas)
      })
      .ToList();

      return Ok(result);
    }

    [HttpPost("MakeOrder")]
    public async Task<ActionResult> MakeOrder([FromBody] OrderViewModel order)
    {
      if (!ModelState.IsValid) return BadRequest(ModelState);

      var mappedModel = _mapper.Map<OrderDTO>(order);
      await _orderService.MakeOrderAsync(mappedModel);

      return Ok();
    }

    [HttpPost("DeleteOrder")]
    public async Task<ActionResult> DeleteOrder([FromBody] int Id)
    {
      await _orderService.DeleteOrderByIdAsync(Id);
      return Ok();
    }
  }
}
