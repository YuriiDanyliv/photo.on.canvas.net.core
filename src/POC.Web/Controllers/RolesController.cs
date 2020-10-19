using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using POC.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using POC.Web.ViewModel;
using PCO.Web.ViewModel;

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
    public async Task<ActionResult<IdentityResult>> CreateRole([FromBody] string name)
    {
      var result = await _rolesService.CreateRoleAsync(name);
      if (result.Succeeded) return Ok(result);

      return BadRequest(result.Errors);
    }

    [HttpPost("DeleteRole")]
    public async Task<ActionResult<IdentityResult>> DeleteRole([FromBody] string id)
    {
      var result = await _rolesService.DeleteRoleAsync(id);
      if (result.Succeeded) return Ok(result);

      return BadRequest(result.Errors);
    }

    [HttpPost("GetRoles")]
    public ActionResult<IdentityRole> GetRoles()
    {
      var result = _rolesService.GetRoles();
      return Ok(result);
    }

    [HttpPost("GetUserRoles")]
    public async Task<ActionResult<UserViewModel>> GetUserRoles([FromBody] string userId)
    {
      var result = _mapper.Map<UserViewModel>(await _rolesService.GetUserRolesAsync(userId));
      return Ok(result);
    }

    [HttpPost("EditUserRoles")]
    public async Task<ActionResult<IdentityResult>> EditUserRoles([FromBody] EditRolesViewModel model)
    {
      var result = await _rolesService.EditUserRolesAsync(model.UserId, model.Roles);
      if (result.AddResult.Succeeded && result.RemoveResult.Succeeded) return Ok();

      return BadRequest(result);
    }

  }
}
