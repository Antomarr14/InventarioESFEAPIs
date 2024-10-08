using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Implementaciones;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TipoController : ControllerBase
    {
        private readonly ITipoService _tipoService;

        public TipoController(ITipoService tipoService)
        {
            _tipoService = tipoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipo>>> GetTipo()
        {
            var tipo = await _tipoService.GetTipos();
            return Ok(tipo);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetTipo(int Id)
        {
            var tipo = await _tipoService.GetTipoById(Id);
            if (tipo == null)
            {
                return NotFound();
            }
            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<Tipo>> CreteTipo([FromBody] Tipo tipo)
        {
            tipo.Id = 0;

            var tipoCreada = await _tipoService.CreateTipo(tipo);
            return CreatedAtAction(nameof(GetTipo), new { id = tipoCreada.Id }, tipoCreada);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateTipo(int Id, [FromBody] Tipo tipo)
        {
            await _tipoService.UpdateTipo(tipo, Id);
            return NoContent();
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> SuprimirTipo(int Id)
        {
            try
            {
                var tipo = await _tipoService.SuprimirTipoAsync(Id);
                if (tipo == null)
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

