using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using POC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using POC.Web.ViewModel;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace WEB.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GalleryController : Controller
  {
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    
    public GalleryController(IFileService fileService, IMapper mapper)
    {
      _fileService = fileService;
      _mapper = mapper;
    }

    [HttpGet("GetImages")]
    public async Task<ActionResult<List<GalleryGetImageVM>>> GetImages()
    {
      var images = await _fileService.GetFiles("Gallery");
      var result = _mapper.Map<List<GalleryGetImageVM>>(images);

      return Ok(result);
    }

    [HttpPost("AddImage")]
    public async Task<ActionResult> AddImage([FromForm] GalleryAddImageVM data)
    {
      await _fileService.AddFile(data.Image, "Gallery");

      return Ok();
    }

    [HttpDelete("DeleteImage")]
    public async Task<ActionResult> DeleteImage([FromQuery] string imageId)
    {
      await _fileService.DeleteFile(imageId);

      return Ok();
    }

  }
}
