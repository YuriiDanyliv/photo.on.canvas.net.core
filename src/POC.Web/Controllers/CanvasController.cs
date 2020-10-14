using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using POC.BLL.DTO;
using POC.BLL.Interfaces;
using POC.Web.ViewModel;
using System.Threading.Tasks;

namespace POC.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CanvasController : Controller
  {
    private readonly ILogger<CanvasController> _logger;
    private readonly ICanvasService _canvasService;
    private readonly IMapper _mapper;

    public CanvasController(
      ILogger<CanvasController> logger,
      ICanvasService canvasService,
      IMapper mapper)
    {
      _logger = logger;
      _canvasService = canvasService;
      _mapper = mapper;
    }

    [HttpPost("GetCanvas")]
    public IActionResult GetCanvas()
    {
      var result = _canvasService.GetCanvas();
      return Ok(result);
    }

    [HttpPost("CreateCanvas")]
    public async Task<IActionResult> CreateCanvas([FromBody] CanvasViewModel model)
    {
      var mappedModel = _mapper.Map<CanvasDTO>(model);
      await _canvasService.CreateCanvasAsync(mappedModel);

      _logger.LogInformation("Create canvas action executed");

      return Ok();
    }

    [HttpPost("DeleteCanvas")]
    public IActionResult DeleteCanvas([FromBody] CanvasViewModel model)
    {
      var mappedModel = _mapper.Map<CanvasDTO>(model);
      _canvasService.DeleteCanvasAsync(mappedModel);

      return Ok();
    }
  }
}
