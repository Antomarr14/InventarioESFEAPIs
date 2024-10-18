using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventarioESFEAPIs.DTO;
using Microsoft.AspNetCore.Authorization;

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

        // GET: api/articulo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloDTO>>> GetArticulos()
        {
            var articulos = await _articuloService.GetArticulos();
            return Ok(articulos);
        }

        // GET: api/articulo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ArticuloDTO>> GetArticulo(int id)
        {
            var articulo = await _articuloService.GetArticuloById(id);
            if (articulo == null)
            {
                return NotFound();
            }
            return Ok(articulo);
        }

        // POST: api/articulo
        [HttpPost]
        public async Task<ActionResult<ArticuloDTO>> CreateArticulo([FromBody] ArticuloDTO articuloDto)
        {
            if (articuloDto == null)
            {
                return BadRequest();
            }

            var nuevoArticulo = await _articuloService.CreateArticulo(articuloDto);
            return CreatedAtAction(nameof(GetArticulo), new { id = nuevoArticulo.Id }, nuevoArticulo);
        }

        // PUT: api/articulo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticulo(int id, [FromBody] ArticuloDTO articuloDto)
        {
            if (id != articuloDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _articuloService.UpdateArticulo(articuloDto, id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _articuloService.GetArticuloById(id) == null)
                {
                    return NotFound();
                }
                throw; // Re-lanzar la excepción
            }

            return NoContent();
        }

        // DELETE: api/articulo/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ArticuloDTO>> SuprimirArticulo(int id)
        {
            var articuloEliminado = await _articuloService.SuprimirArticuloAsync(id);
            if (articuloEliminado == null)
            {
                return NotFound();
            }
            return Ok(articuloEliminado);
        }
    }
}