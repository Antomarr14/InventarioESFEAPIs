using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioESFEAPIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ImagenController : ControllerBase
    {
        private readonly IImagenService _imagenService;

        public ImagenController(IImagenService imagenService)
        {
            _imagenService = imagenService;
        }

        // GET: api/Imagen
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenDTO>>> GetImagenes()
        {
            var imagenes = await _imagenService.GetImagenesAsync();
            return Ok(imagenes);
        }

        // GET: api/Imagen/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenDTO>> GetImagenById(int id)
        {
            var imagen = await _imagenService.GetImagenByIdAsync(id);
            if (imagen == null)
            {
                return NotFound(); // 404 si no se encuentra la imagen
            }

            return Ok(imagen);
        }

        // POST: api/Imagen
        [HttpPost]
        public async Task<ActionResult<ImagenDTO>> CrearImagen([FromBody] ImagenDTO imagenDTO)
        {
            if (imagenDTO == null)
            {
                return BadRequest("ImagenDTO is null"); // 400 si el DTO es nulo
            }

            var createdImagen = await _imagenService.CrearImagenAsync(imagenDTO);
            return CreatedAtAction(nameof(GetImagenById), new { id = createdImagen.Id }, createdImagen);
        }

        // PUT: api/Imagen/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateImagen(int id, [FromBody] ImagenDTO imagenDTO)
        {
            if (id != imagenDTO.Id)
            {
                return BadRequest("ID mismatch"); // 400 si el ID no coincide
            }

            try
            {
                await _imagenService.UpdateImagenAsync(imagenDTO);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 si no se encuentra la imagen
            }

            return NoContent(); // 204 si la actualización es exitosa
        }

        // DELETE: api/Imagen/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteImagen(int id)
        {
            try
            {
                await _imagenService.DeleteImagenAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); // 404 si no se encuentra la imagen
            }

            return NoContent(); // 204 si la eliminación es exitosa
        }
    }
}
