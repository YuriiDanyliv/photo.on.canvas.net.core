using Microsoft.AspNetCore.Mvc;
using POC.BLL.Dto;
using POC.BLL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POC.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanvasController : Controller
    {
        private readonly ICanvasService _canvasService;

        public CanvasController(
          ICanvasService canvasService)
        {
            _canvasService = canvasService;
        }

        /// <summary>
        /// Returns list of Canvases
        /// </summary>
        /// <returns>List of CanvasDto</returns>
        [HttpGet("GetCanvases")]
        public async Task<ActionResult<List<CanvasDto>>> GetCanvases()
        {
            var result = await _canvasService.GetCanvasesAsync();
            if (result.Count == 0) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Creates a canvas
        /// </summary>
        /// <param name="canvas">Canvas model</param>
        /// <returns></returns>
        [HttpPost("CreateCanvas")]
        public async Task<ActionResult> CreateCanvas([FromForm] CreateCanvasDto canvas)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _canvasService.CreateCanvasAsync(canvas);
            return Ok("Canvas created");
        }

        /// <summary>
        /// Deletes canvas by its id
        /// </summary>
        /// <param name="Id">Canvas ID</param>
        /// <returns></returns>
        [HttpDelete("DeleteCanvas")]
        public async Task<ActionResult> DeleteCanvasById([FromForm] string Id)
        {
            if (Id == null) return BadRequest("No ID");

            await _canvasService.DeleteCanvasByIdAsync(Id);
            return Ok("Canvas deleted");
        }
    }
}
