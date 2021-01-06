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

    [HttpPost("GetStories")]
    public async Task<ActionResult<IList<DAL.Entities.InstagramMedia>>> GetStories([FromForm] string name)
    {
      if (name == null) return Ok(await _instaService.GetStoriesAsync());
      return Ok(await _instaService.GetStoriesAsync(name));
    }

    [HttpGet("UpdateDbInstaDataAsync")]
    public async Task<ActionResult> UpdateDbInstaDataAsync()
    {
      await _instaService.UpdateDbInstaDataAsync();
      return Ok();
    }
  }
}