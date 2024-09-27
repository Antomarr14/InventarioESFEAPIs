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
            return Ok(compra);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCompra(int id)
        {
            var compra = await _compraService.GetCompraById(id);
            if (compra == null)
            {
                return NotFound();
            }
            return Ok(compra);
        }

        [HttpPost]
        public async Task<ActionResult<Compra>> CreateProveedor([FromBody] Compra compra)
        {
            compra.Id = 0;

            var compraCreado = await _compraService.CreateCompra(compra);
            return CreatedAtAction(nameof(GetCompra), new { id = compraCreado.Id }, compraCreado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompra(int id, [FromBody] Compra compra)
        {
            await _compraService.UpdateCompra(compra, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecompra(int id)
        {
            await _compraService.DeleteCompra(id);
            return NoContent();
        }
    }
}
