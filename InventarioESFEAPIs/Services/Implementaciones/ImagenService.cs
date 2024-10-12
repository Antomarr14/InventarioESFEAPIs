using InventarioESFEAPIs.Context;
using InventarioESFEAPIs.DTO;
using InventarioESFEAPIs.Models;
using InventarioESFEAPIs.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventarioESFEAPIs.Services
{
    public class ImagenService : IImagenService
    {
        private readonly InventarioESFEContext _context; 

        public ImagenService(InventarioESFEContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ImagenDTO>> GetImagenesAsync()
        {
            return await _context.Imagen
                .Select(imagen => new ImagenDTO
                {
                    Id = imagen.Id,
                    Url = imagen.Url,
                    ImageData = imagen.ImageData != null ? Convert.ToBase64String(imagen.ImageData) : null,
                    IdAsignacionCodigo = imagen.IdAsignacionCodigo
                })
                .ToListAsync();
        }

        public async Task<ImagenDTO> GetImagenByIdAsync(int id)
        {
            var imagen = await _context.Imagen.FindAsync(id);
            if (imagen == null)
            {
                return null;
            }

            return new ImagenDTO
            {
                Id = imagen.Id,
                Url = imagen.Url,
                ImageData = imagen.ImageData != null ? Convert.ToBase64String(imagen.ImageData) : null,
                IdAsignacionCodigo = imagen.IdAsignacionCodigo
            };
        }

        public async Task<IEnumerable<ImagenDTO>> GetImagenesByArticuloIdAsync(int articuloId)
        {
            return await _context.Imagen
                .Where(i => i.AsignacionCodigo.IdArticulo == articuloId) // Cambia 'IdArticulo' por el nombre correcto de la propiedad
                .Select(imagen => new ImagenDTO
                {
                    Id = imagen.Id,
                    Url = imagen.Url,
                    ImageData = imagen.ImageData != null ? Convert.ToBase64String(imagen.ImageData) : null,
                    IdAsignacionCodigo = imagen.IdAsignacionCodigo
                })
                .ToListAsync();
        }

        public async Task<ImagenDTO> CrearImagenAsync(ImagenDTO imagenDTO)
        {
            try
            {
                // Limpiar la cadena Base64
                string cleanedBase64 = imagenDTO.ImageData?.Replace(" ", "").Replace("\n", "").Replace("\r", "") ?? "";

                // Ajustar el padding si es necesario
                int padding = cleanedBase64.Length % 4;
                if (padding > 0)
                {
                    cleanedBase64 += new string('=', 4 - padding);
                }

                // Convertir la cadena Base64 a bytes
                byte[] imageBytes = Convert.FromBase64String(cleanedBase64);
                
                var nuevaImagen = new Imagen
                {
                    Url = imagenDTO.Url,
                    ImageData = imageBytes,
                    IdAsignacionCodigo = imagenDTO.IdAsignacionCodigo // Usar la propiedad correcta
                };

                _context.Imagen.Add(nuevaImagen);
                await _context.SaveChangesAsync();

                imagenDTO.Id = nuevaImagen.Id; // Asignar el ID generado
                return imagenDTO;
            }
            catch (FormatException ex)
            {
                throw new InvalidOperationException("La cadena no es una imagen válida en Base64.", ex);
            }
        }

        public async Task UpdateImagenAsync(ImagenDTO imagenDTO)
{
    try
    {
        // Buscar la imagen por ID
        var imagen = await _context.Imagen.FindAsync(imagenDTO.Id);
        if (imagen == null)
        {
            throw new KeyNotFoundException($"Imagen con ID {imagenDTO.Id} no encontrada.");
        }

        // Actualizar propiedades de la imagen
        imagen.Url = imagenDTO.Url;

        // Validar y convertir la cadena Base64 a bytes si existe
        if (!string.IsNullOrEmpty(imagenDTO.ImageData) && imagenDTO.ImageData != Convert.ToBase64String(imagen.ImageData))
        {
            string cleanedBase64 = imagenDTO.ImageData.Replace(" ", "").Replace("\n", "").Replace("\r", "");

            // Ajustar el padding si es necesario
            int padding = cleanedBase64.Length % 4;
            if (padding > 0)
            {
                cleanedBase64 += new string('=', 4 - padding);
            }

            // Asegurarse de que solo contenga caracteres válidos
            if (!IsBase64String(cleanedBase64))
            {
                throw new InvalidOperationException("La cadena contiene caracteres no válidos para Base64.");
            }

            // Convertir a bytes
            imagen.ImageData = Convert.FromBase64String(cleanedBase64);
        }

        // Actualizar IdAsignacionCodigo directamente
        imagen.IdAsignacionCodigo = imagenDTO.IdAsignacionCodigo;

        // Guardar los cambios
        await _context.SaveChangesAsync();
    }
    catch (FormatException ex)
    {
        throw new InvalidOperationException("La cadena no es una imagen válida en Base64.", ex);
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException("Ocurrió un error al actualizar la imagen.", ex);
    }
}


// Método auxiliar para validar si una cadena es Base64 válida
private bool IsBase64String(string base64)
{
    Span<byte> buffer = new Span<byte>(new byte[base64.Length]);
    return Convert.TryFromBase64String(base64, buffer, out _);
}

        public async Task DeleteImagenAsync(int id)
        {
            var imagen = await _context.Imagen.FindAsync(id);
            if (imagen == null)
            {
                throw new KeyNotFoundException($"Imagen con ID {id} no encontrada.");
            }

            _context.Imagen.Remove(imagen);
            await _context.SaveChangesAsync();
        }
    }
}
