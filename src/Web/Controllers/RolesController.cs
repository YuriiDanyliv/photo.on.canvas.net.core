using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using POC.BLL.Models;
using POC.BLL.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : Controller
    {
        private readonly IRolesService _rolesService;

        public RolesController(
            IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        /// <summary>
        /// Get all exist roles 
        /// </summary>
        /// <returns>List of roles</returns>
        [HttpGet("GetAvailableRoles")]
        public ActionResult<IdentityRole> GetAvailableRoles()
        {
            var result = _rolesService.GetRoles();
            if (result == null || result.Count == 0) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Get all users roles by user ID
        /// </summary>
        /// <param name="userId">user ID</param>
        /// <returns>List of users roles</returns>
        [HttpPost("GetUserRoles")]
        public async Task<ActionResult<IList<string>>> GetUserRoles([FromForm] string userId)
        {
            if (userId == null) return BadRequest("Inavalid user ID");
            var result = await _rolesService.GetUserRolesAsync(userId);
            if (result == null || result.Count == 0) return NoContent();
            return Ok(result);
        }

        /// <summary>
        /// Change users roles
        /// </summary>
        /// <param name="model">user id and list of applying roles</param>
        /// <returns>Identity result</returns>
        [HttpPut("EditUserRoles")]
        public async Task<ActionResult<IdentityResult>> EditUserRoles([FromForm] EditRolesModel model)
        {
            var result = await _rolesService.EditUserRolesAsync(model);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
    }
}
