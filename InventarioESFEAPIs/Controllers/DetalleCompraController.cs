using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleCompraController : ControllerBase
    {
        private readonly IDetalleCompraService _detalleCompraService;

        public DetalleCompraController(IDetalleCompraService detalleCompraService)
        {
            _detalleCompraService = detalleCompraService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompra>>> GetDetalleCompra()
        {
            var detalleCompra = await _detalleCompraService.GetDetalleCompras();
            return Ok(detalleCompra);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetDetalleCompra(int id)
        {
            var detalleCompra = await _detalleCompraService.GetDetalleCompraById(id);
            if (detalleCompra == null)
            {
                return NotFound();
            }
            return Ok(detalleCompra);
        }

        [HttpPost]
        public async Task<ActionResult<DetalleCompra>> CreateDetalleCompra([FromBody] DetalleCompra detalleCompra)
        {
            detalleCompra.Id = 0;

            var detalleCompraCreado = await _detalleCompraService.CreateDetalleCompra(detalleCompra);
            return CreatedAtAction(nameof(GetDetalleCompra), new { id = detalleCompra.Id }, detalleCompraCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDetalleCompra(int id, [FromBody] DetalleCompra detalleCompra)
        {
            await _detalleCompraService.UpdateDetalleCompra(detalleCompra, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletedetalleCompra(int id)
        {
            await _detalleCompraService.DeleteDetalleCompra(id);
            return NoContent();
        }
    }
}
