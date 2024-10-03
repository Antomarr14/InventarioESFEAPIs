using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolControlController : ControllerBase
    {
        private readonly IRolControlService _rolcontrolservice;

        public RolControlController(IRolControlService rolcontrolservice)
        {
            _rolcontrolservice = rolcontrolservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolControl>>> GetRolControl()
        {
            var rolcontrol = await _rolcontrolservice.GetRolControl();
            return Ok(rolcontrol);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRolControl(int id)
        {
            var rolcontrol = await _rolcontrolservice.GetRolControlById(id);
            if (rolcontrol == null)
            {
                return NotFound();
            }
            return Ok(rolcontrol);
        }

        [HttpPost]
        public async Task<ActionResult<RolControl>> CreateRolControl([FromBody] RolControl rolcontrol)
        {
            rolcontrol.Id = 0;

            var rolcontrolCreado = await _rolcontrolservice.CreateRolControl(rolcontrol);
            return CreatedAtAction(nameof(GetRolControl), new { id = rolcontrolCreado.Id }, rolcontrolCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, [FromBody] RolControl rolcontrol)
        {
            await _rolcontrolservice.UpdateRolControl(rolcontrol, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRolControl(int id)
        {
            await _rolcontrolservice.DeleteRolControl(id);
            return NoContent();
        }
    }
}
