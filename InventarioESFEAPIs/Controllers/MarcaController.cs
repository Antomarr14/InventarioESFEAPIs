using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaservice;

        public MarcaController(IMarcaService marcaservice)
        {
            _marcaservice = marcaservice;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Marca>>> GetMarca()
        {
            var marca = await _marcaservice.GetMarca();
            return Ok(marca);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetMarca(int id)
        {
            var marca = await _marcaservice.GetMarcaById(id);
            if (marca == null)
            {
                return NotFound();
            }
            return Ok(marca);
        }

        [HttpPost]
        public async Task<ActionResult<Marca>> CreateMarca([FromBody] Marca marca)
        {
            marca.Id = 0;

            var marcaCreado = await _marcaservice.CreateMarca(marca);
            return CreatedAtAction(nameof(GetMarca), new { id = marcaCreado.Id }, marcaCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMarca(int id, [FromBody] Marca marca)
        {
            await _marcaservice.UpdateMarca(marca, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMarca(int id)
        {
            await _marcaservice.DeleteMarca(id);
            return NoContent();
        }

    }
}
