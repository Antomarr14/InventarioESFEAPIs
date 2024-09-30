using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
   
    public class UsuarioRolController : ControllerBase
    {
        private readonly IUsuarioRolService  _usuariorol;

        public UsuarioRolController(IUsuarioRolService usuariorolservice)
        {
            _usuariorol = usuariorolservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioRol>>> GetUsuarioRol()
        {
            var usuariorol = await _usuariorol.GetUsuarioRol();
            return Ok(usuariorol);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuarioRol(int id)
        {
            var usuariorol = await _usuariorol.GetUsuarioRolById(id);
            if (usuariorol == null)
            {
                return NotFound();
            }
            return Ok(usuariorol);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioRol>> CreateUsuarioRol([FromBody] UsuarioRol usuariorol)
        {
            usuariorol.Id = 0;

            var usuariorolCreado = await _usuariorol.CreateUsuarioRol(usuariorol);
            return CreatedAtAction(nameof(GetUsuarioRol), new { id = usuariorolCreado.Id }, usuariorolCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuarioRol(int id, [FromBody] UsuarioRol usuariorol)
        {
            await _usuariorol.UpdateUsuarioRol(usuariorol, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuarioRol(int id)
        {
            await _usuariorol.DeleteUsuarioRol(id);
            return NoContent();
        }

    }
}
