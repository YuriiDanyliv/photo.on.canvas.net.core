using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AutoMapper;
using POC.Web.ViewModel;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using POC.DAL.Entities;
using POC.BLL.Services;

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
    public ActionResult<IQueryable<CanvasResponseVM>> GetCanvas()
    {
      var result = _canvasService.GetCanvas()
      .ProjectTo<CanvasResponseVM>(_mapper.ConfigurationProvider)
      .OrderBy(x => x.Price);
      
      return Ok(result);
    }

    [HttpPost("CreateCanvas")]
    public async Task<ActionResult> CreateCanvas([FromBody] CreateCanvasVM model)
    {
      var canvas = _mapper.Map<Canvas>(model);
      await _canvasService.CreateCanvasAsync(canvas);

      _logger.LogInformation("Create canvas action executed");

      return Ok();
    }

    [HttpPost("DeleteCanvas")]
    public async Task<ActionResult> DeleteCanvasById([FromBody] string Id)
    {
      await _canvasService.DeleteCanvasByIdAsync(Id);
      return Ok();
    }
  }
}
