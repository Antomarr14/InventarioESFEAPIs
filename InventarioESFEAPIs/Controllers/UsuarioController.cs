using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Implementaciones;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Utilizaremos Authorize para proteger todas las acciones
                // esto debe ir en todos los controladores
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            var usuarios = await _usuarioService.GetUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUsuario(int id)
        {
            var usuario = await _usuarioService.GetUsuarioById(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> CreateUsuario([FromBody] Usuario usuario)
        {
            // Si el usuario tiene un ID especificado, lo ignoramos
            usuario.Id = 0; // Reseteamos el ID a 0 para asegurarnos de que no se use

            // Crea el nuevo usuario
            var usuarioCreado = await _usuarioService.CreateUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuario), new { id = usuarioCreado.Id }, usuarioCreado);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            await _usuarioService.UpdateUsuario(usuario, id);
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> SuprimirUsuario(int Id)
        {
            try
            {
                var usuario = await _usuarioService.SuprimirUsuarioAsync(Id);
                if (usuario == null)
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
