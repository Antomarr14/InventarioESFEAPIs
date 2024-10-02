using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
        {
            var categoria = await _categoriaService.GetCategorias();
            return Ok(categoria);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetCategoria(int Id)
        {
            var categoria = await _categoriaService.GetCategoriaById(Id);
            if(categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> CreteCategoria([FromBody] Categoria categoria)
        {
            categoria.Id = 0;

            var categoriaCreada = await _categoriaService.CreateCategoria(categoria);
            return CreatedAtAction(nameof(GetCategoria), new { id = categoriaCreada.Id}, categoriaCreada);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategoria(int Id, [FromBody] Categoria categoria)
        {
            await _categoriaService.UpdateCategoria(categoria, Id);
            return NoContent();
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            await _categoriaService.DeleteCategoria(id);
            return NoContent();
        }
    }
}