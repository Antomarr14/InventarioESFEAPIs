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
    public class EstadoController : ControllerBase
    {
        private readonly IEstadoService _estadoservice;

        public EstadoController(IEstadoService estadoservice)
        {
            _estadoservice = estadoservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estado>>> GetEstado()
        {
            var estado = await _estadoservice.GetEstado();
            return Ok(estado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetEstado(int id)
        {
            var estado = await _estadoservice.GetEstadoById(id);
            if (estado == null)
            {
                return NotFound();
            }
            return Ok(estado);
        }

        [HttpPost]
        public async Task<ActionResult<Estado>> CreateEstado([FromBody] Estado estado)
        {
            estado.Id = 0;

            var estadoCreado = await _estadoservice.CreateEstado(estado);
            return CreatedAtAction(nameof(GetEstado), new { id = estadoCreado.Id }, estadoCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] Estado estado)
        {
            await _estadoservice.UpdateEstado(estado, id);
            return NoContent();
        }


        [HttpPatch("{Id}")]
        public async Task<IActionResult> SuprimirEstado(int Id)
        {
            try
            {
                var estado = await _estadoservice.SuprimirEstadoAsync(Id);
                if (estado == null)
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
