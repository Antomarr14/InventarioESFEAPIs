using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerdidaController : ControllerBase
    {
        private readonly IPerdidasService _perdidasService;

        public PerdidaController(IPerdidasService perdidasService)
        {
            _perdidasService = perdidasService;
        }

        // GET: api/Perdida
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPerdidas()
        {
            var perdidas = await _perdidasService.GetPerdidas();

            if (perdidas == null || !perdidas.Any())
            {
                return NotFound("No se encontraron pérdidas.");
            }

            // Devolver la lista de pérdidas
            var response = perdidas.Select(p => new
            {
                p.Id,
                FechaPerdida = p.FechaPerdida.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                p.CausaPerdida,
                p.ValorPerdida,
                p.IdArticulo,
                p.IdUsuario,
                p.IdCompra
            });

            return Ok(response);
        }

        // GET: api/Perdida/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPerdidaById(int id)
        {
            var perdida = await _perdidasService.GetPerdidaById(id);

            if (perdida == null)
            {
                return NotFound("Pérdida no encontrada.");
            }

            // Devolver la pérdida con detalles
            var response = new
            {
                perdida.Id,
                FechaPerdida = perdida.FechaPerdida.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                perdida.CausaPerdida,
                perdida.ValorPerdida,
                perdida.IdArticulo,
                perdida.IdUsuario,
                perdida.IdCompra
            };

            return Ok(response);
        }

        // POST: api/Perdida
        [HttpPost]
        public async Task<ActionResult<Perdidas>> CreatePerdida(Perdidas perdida)
        {
            // Validar que la pérdida tenga una fecha y causa
            if (perdida.FechaPerdida == DateTime.MinValue || string.IsNullOrWhiteSpace(perdida.CausaPerdida))
            {
                return BadRequest("La fecha de pérdida y la causa son requeridas.");
            }

            // Crear la pérdida
            var createdPerdida = await _perdidasService.CreatePerdida(perdida);
            return CreatedAtAction(nameof(GetPerdidaById), new { id = createdPerdida.Id }, createdPerdida);
        }

        // PUT: api/Perdida/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePerdida(int id, Perdidas perdida)
        {
            if (id != perdida.Id)
            {
                return BadRequest("El ID proporcionado no coincide con la pérdida a actualizar.");
            }

            try
            {
                await _perdidasService.UpdatePerdida(perdida, id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pérdida no encontrada.");
            }

            return NoContent();
        }

        // DELETE: api/Perdida/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerdida(int id)
        {
            try
            {
                await _perdidasService.DeletePerdida(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Pérdida no encontrada.");
            }

            return NoContent();
        }
    }
}
