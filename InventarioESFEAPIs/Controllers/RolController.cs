using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Implementaciones;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolservice;

        public RolController(IRolService rolservice)
        {
            _rolservice = rolservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rol>>> GetRol()
        {
            var rol = await _rolservice.GetRol();
            return Ok(rol);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetRol(int id)
        {
            var rol = await _rolservice.GetRolById(id);
            if (rol == null)
            {
                return NotFound();
            }
            return Ok(rol);
        }

        [HttpPost]
        public async Task<ActionResult<Rol>> CreateRol([FromBody] Rol rol)
        {
            rol.Id = 0;

            var rolCreado = await _rolservice.CreateRol(rol);
            return CreatedAtAction(nameof(GetRol), new { id = rolCreado.Id }, rolCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRol(int id, [FromBody] Rol rol)
        {
            await _rolservice.UpdateRol(rol, id);
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> SuprimirRol(int Id)
        {
            try
            {
                var rol = await _rolservice.SuprimirRolAsync(Id);
                if (rol == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (KeyNotFoundException knfEx)
            {
                return NotFound(knfEx.Message);
            }

        }
    }
}

