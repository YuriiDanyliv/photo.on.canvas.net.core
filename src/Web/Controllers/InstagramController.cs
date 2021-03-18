using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.BLL.Dto;
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

        /// <summary>
        /// Returns Instagram media data by name or returns all media data if no name is specified
        /// </summary>
        /// <param name="name">name of media category</param>
        /// <returns>List of instagram media</returns>
        [HttpPost("GetStories")]
        public async Task<ActionResult<IList<InstagramMediaDto>>> GetStories([FromForm] string name)
        {
            IList<InstagramMediaDto> result;
            
            if (name == null)
            {
                result = await _instaService.GetStoriesAsync();
                if (result.Count == 0) return NoContent();
                return Ok(result);
            }

            result = await _instaService.GetStoriesAsync(name);
            if (result.Count == 0) return NoContent();
            return Ok(result);
        }


        /// <summary>
        /// Updates instagram media data in database
        /// </summary>
        /// <returns></returns>
        [HttpGet("UpdateDbInstaDataAsync")]
        public async Task<ActionResult> UpdateDbInstaDataAsync()
        {
            await _instaService.UpdateDbInstaDataAsync();
            return Ok();
        }
    }
}