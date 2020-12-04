using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.BLL.Model;
using POC.BLL.Services;

namespace POC.Web.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InstagramController : Controller
  {
    private readonly IInstagramService _instaService;

    public InstagramController(IInstagramService instaService)
    {
      _instaService = instaService;
    }

    [HttpGet("GetStories")]
    public async Task<ActionResult<List<InstagramMediaModel>>> GetStories([FromQuery] string storyName)
    {
      var stories = await _instaService.GetStoriesByNameAsync(storyName);
      return Ok(stories);
    }
  }
}