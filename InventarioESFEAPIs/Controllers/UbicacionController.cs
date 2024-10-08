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
    public class UbicacionController : ControllerBase
    {
        private readonly IUbicacionService _UbicacionService;

        public UbicacionController(IUbicacionService ubicacionService)
        {
            _UbicacionService = ubicacionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ubicacion>>> GetUbicacion()
        {
            var ubicacion = await _UbicacionService.GetUbicacions();
            return Ok(ubicacion);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetUbicacion(int Id)
        {
            var ubicacion = await _UbicacionService.GetUbicacionById(Id);
            if (ubicacion == null)
            {
                return NotFound();
            }
            return Ok(ubicacion);
        }

        [HttpPost]
        public async Task<ActionResult<Ubicacion>> CreteUbicacion([FromBody] Ubicacion ubicacion)
        {
            ubicacion.Id = 0;

            var UbicacionCreada = await _UbicacionService.CreateUbicacion(ubicacion);
            return CreatedAtAction(nameof(GetUbicacion), new { id = UbicacionCreada.Id }, UbicacionCreada);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateUbicacion(int Id, [FromBody] Ubicacion ubicacion)
        {
            await _UbicacionService.UpdateUbicacion(ubicacion, Id);
            return NoContent();
        }
        [HttpPatch("{Id}")]
        public async Task<IActionResult> SuprimirUbicacion(int Id)
        {
            try
            {
                var ubicacion = await _UbicacionService.SuprimirUbicacionAsync(Id);
                if (ubicacion == null)
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

