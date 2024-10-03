using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace InventarioESFEAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        // GET: api/Prestamo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPrestamos()
        {
            var prestamos = await _prestamoService.GetPrestamos();

            // Verificar si prestamos es null o vacío
            if (prestamos == null || !prestamos.Any())
            {
                return NotFound("No se encontraron préstamos.");
            }

            // Devolver los préstamos y formatear las fechas en el resultado
            var response = prestamos.Select(p => new
            {
                p.Id,
                FechaPrestamo = p.FechaPrestamo.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                FechaDevolucion = p.FechaDevolucion.HasValue
                    ? p.FechaDevolucion.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                    : null,
                p.IdEstado,
                p.IdUsuario,
                p.IdArticulo
            });

            return Ok(response);
        }

        // GET: api/Prestamo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPrestamoById(int id)
        {
            var prestamo = await _prestamoService.GetPrestamoById(id);

            if (prestamo == null)
            {
                return NotFound("Préstamo no encontrado.");
            }

            // Formatear la fecha para la respuesta
            var response = new
            {
                prestamo.Id,
                FechaPrestamo = prestamo.FechaPrestamo.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture),
                FechaDevolucion = prestamo.FechaDevolucion.HasValue
                    ? prestamo.FechaDevolucion.Value.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                    : null,
                prestamo.IdEstado,
                prestamo.IdUsuario,
                prestamo.IdArticulo
            };

            return Ok(response);
        }

        // POST: api/Prestamo
        [HttpPost]
        public async Task<ActionResult<Prestamo>> CreatePrestamo(Prestamo prestamo)
        {
            // Validar que se haya proporcionado la fecha de devolución
            if (!prestamo.FechaDevolucion.HasValue)
            {
                return BadRequest("Por favor, indique la fecha y hora en la que se realizará la devolución.");
            }

            // Establecer la fecha de préstamo a la fecha actual
            prestamo.FechaPrestamo = DateTime.UtcNow;

            // Validar que la fecha de devolución no sea anterior a la fecha de préstamo
            if (prestamo.FechaDevolucion.Value < prestamo.FechaPrestamo)
            {
                return BadRequest("La fecha de devolución no puede ser anterior a la fecha del préstamo.");
            }

            // Crear el préstamo
            var createdPrestamo = await _prestamoService.CreatePrestamo(prestamo);
            return CreatedAtAction(nameof(GetPrestamos), new { id = createdPrestamo.Id }, createdPrestamo);
        }

        // PUT: api/Prestamo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrestamo(int id, Prestamo prestamo)
        {
            if (id != prestamo.Id)
            {
                return BadRequest();
            }

            try
            {
                await _prestamoService.UpdatePrestamo(prestamo, id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Prestamo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrestamo(int id)
        {
            try
            {
                await _prestamoService.DeletePrestamo(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
