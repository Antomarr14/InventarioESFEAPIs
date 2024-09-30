using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ControlController : ControllerBase
    {
        private readonly IControlService _controlservice;

        public ControlController(IControlService controlservice)
        {
            _controlservice = controlservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Control>>> GetControl()
        {
            var control = await _controlservice.GetControl();
            return Ok(control);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetControl(int id)
        {
            var control = await _controlservice.GetControlById(id);
            if (control == null)
            {
                return NotFound();
            }
            return Ok(control);
        }

        [HttpPost]
        public async Task<ActionResult<Compra>> CreateControl([FromBody] Control control)
        {
            control.Id = 0;

            var controlCreado = await _controlservice.CreateControl(control);
            return CreatedAtAction(nameof(GetControl), new { id = controlCreado.Id }, controlCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateControl(int id, [FromBody] Control control)
        {
            await _controlservice.UpdateControl(control, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControl(int id)
        {
            await _controlservice.DeleteControl(id);
            return NoContent();
        }

    }
}
