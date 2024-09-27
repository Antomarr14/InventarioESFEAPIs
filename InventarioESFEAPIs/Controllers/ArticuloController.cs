using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class ArticuloController : ControllerBase
    {
        private readonly IArticuloService _articuloService;

        public ArticuloController(IArticuloService articuloService)
        {
            _articuloService = articuloService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Articulo>>> GetArticulos()
        {
            var articulos = await _articuloService.GetArticulos();
            return Ok(articulos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticulo(int id)
        {
            var articulo = await _articuloService.GetArticuloById(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }

        [HttpPost]
        public async Task<ActionResult<Articulo>> CreateArticulo([FromBody] Articulo articulo)
        {
            // Validar stock mínimo
            if (articulo.StockMinima < 5)
            {
                return BadRequest("El stock mínimo no puede ser menor que 5.");
            }

            // Validar stock
            if (articulo.Stock < 5 || articulo.Stock > 50)
            {
                return BadRequest("El stock debe estar entre 5 y 50.");
            }

            // Validar disponibilidad
            // Aquí se cambia la validación para aceptar bool
            if (articulo.Disponibilidad != true && articulo.Disponibilidad != false)
            {
                return BadRequest("La disponibilidad debe ser verdadero (true) o falso (false).");
            }

            // Reseteamos el ID a 0 para asegurarnos de que no se use
            articulo.Id = 0; 

            var articuloCreado = await _articuloService.CreateArticulo(articulo);
            return CreatedAtAction(nameof(GetArticulo), new { id = articuloCreado.Id }, articuloCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticulo(int id, [FromBody] Articulo articulo)
        {
            // Validar stock mínimo
            if (articulo.StockMinima < 5)
            {
                return BadRequest("El stock mínimo no puede ser menor que 5.");
            }

            // Validar stock
            if (articulo.Stock < 5 || articulo.Stock > 50)
            {
                return BadRequest("El stock debe estar entre 5 y 50.");
            }

            // Validar disponibilidad
            // Aquí se cambia la validación para aceptar bool
            if (articulo.Disponibilidad != true && articulo.Disponibilidad != false)
            {
                return BadRequest("La disponibilidad debe ser verdadero (true) o falso (false).");
            }

            await _articuloService.UpdateArticulo(articulo, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticulo(int id)
        {
            await _articuloService.DeleteArticulo(id);
            return NoContent();
        }
    }
}