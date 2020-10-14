using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using POC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
  public class RolesController : Controller
  {
    private readonly IRolesService _rolesService;
    private readonly IMapper _mapper;
    
    public RolesController(IRolesService rolesService, IMapper mapper)
    {
      _rolesService = rolesService;
      _mapper = mapper;
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole([FromBody] string name)
    {
      var result = await _rolesService.CreateRoleAsync(name);
      if (result.Succeeded) return Ok(result);

      return BadRequest(result.Errors);
    }

    [HttpPost("DeleteRole")]
    public async Task<IActionResult> DeleteRole([FromBody] string id)
    {
      var result = await _rolesService.DeleteRoleAsync(id);
      if (result.Succeeded) return Ok(result);

      return BadRequest(result.Errors);
    }

    [HttpPost("GetRoles")]
    public IActionResult GetRoles()
    {
      var result = _rolesService.GetRoles();
      return Ok(result);
    }

    [HttpPost("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles([FromBody] string userId)
    {
      var result = await _rolesService.GetUserRolesAsync(userId);
      return Ok(result);
    }

    [HttpPost("EditUserRoles")]
    public async Task<IActionResult> EditUserRoles([FromBody] string userId, List<string> roles)
    {
      var result = await _rolesService.EditUserRolesAsync(userId, roles);
      if (result.AddResult.Succeeded && result.RemoveResult.Succeeded) return Ok();

      return BadRequest(result);
    }

  }
}
