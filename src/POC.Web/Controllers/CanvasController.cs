using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using POC.BLL.DTO;
using POC.BLL.Interfaces;
using POC.Web.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;

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

    [HttpGet("GetCanvas")]
    public ActionResult<IQueryable<CanvasViewModel>> GetCanvas()
    {
      var result = _canvasService.GetCanvas().ProjectTo<CanvasViewModel>(_mapper.ConfigurationProvider);
      return Ok(result);
    }

    [HttpPost("CreateCanvas")]
    public async Task<ActionResult> CreateCanvas([FromBody] CanvasViewModel model)
    {
      var mappedModel = _mapper.Map<CanvasDTO>(model);
      await _canvasService.CreateCanvasAsync(mappedModel);

      _logger.LogInformation("Create canvas action executed");

      return Ok();
    }

    [HttpPost("DeleteCanvas")]
    public async Task<ActionResult> DeleteCanvasById([FromBody] int Id)
    {
      await _canvasService.DeleteCanvasByIdAsync(Id);
      return Ok();
    }
  }
}
