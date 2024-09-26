using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedorController : ControllerBase
    {
        private readonly IProveedorService _proveedorService;

        public ProveedorController(IProveedorService proveedorService)
        {
            _proveedorService = proveedorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> GetProveedor()
        {
            var proveedor = await _proveedorService.GetProveedores();
            return Ok(proveedor);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProveedor(int id)
        {
            var proveedor = await _proveedorService.GetProveedorById(id);
            if (proveedor == null)
            {
                return NotFound();
            }
            return Ok(proveedor);
        }

    [HttpPost]
    public async Task<ActionResult<Proveedor>> CreateProveedor([FromBody] Proveedor proveedor)
    {
        // Si el proveedor tiene un ID especificado, lo ignoramos
        proveedor.Id = 0; // Reseteamos el ID a 0 para asegurarnos de que no se use

        var proveedorCreado = await _proveedorService.CreateProveedor(proveedor);
        return CreatedAtAction(nameof(GetProveedor), new { id = proveedorCreado.Id }, proveedorCreado);
    }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProveedor(int id, [FromBody] Proveedor proveedor)
        {
            await _proveedorService.UpdateProveedor(proveedor, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            await _proveedorService.DeleteProveedor(id);
            return NoContent();
        }
    }
}
