using InventarioESFEAPIs.DTO;
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
    public class CompraController : ControllerBase
    {
        private readonly ICompraService _compraService;

        public CompraController(ICompraService compraService)
        {
            _compraService = compraService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compra>>> GetCompra()
        {
            var compra = await _compraService.GetCompras();
                if (compra == null || !compra.Any())
                {
                    return NotFound("No se encontraron compras.");
                }
            return Ok(compra);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCompra(int id)
        {
            var compra = await _compraService.GetCompraById(id);
            if (compra == null)
            {
                return NotFound("No se encontraron compras.");
            }
            return Ok(compra);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompra([FromBody] CompraDTO compraDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _compraService.CreateCompraAsync(compraDTO);
                return Ok(new { Message = "Compra registrada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Ocurrió un error al registrar la compra.", Details = ex.Message });
            }
        }

        [HttpPatch("{Id}")]
        public async Task<IActionResult> SupimirCompra(int Id)
        {
            try
            {
                var compra = await _compraService.SuprimirCompraAsync(Id);
                if (compra == null)
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
