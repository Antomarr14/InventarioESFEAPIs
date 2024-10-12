using System;
using InventarioESFEAPIs.DTO;

namespace InventarioESFEAPIs.Services.Interfaces;

public interface IImagenService
{
    Task<IEnumerable<ImagenDTO>> GetImagenesAsync();
Task<ImagenDTO> GetImagenByIdAsync(int id);
Task<IEnumerable<ImagenDTO>> GetImagenesByArticuloIdAsync(int articuloId);
Task<ImagenDTO> CrearImagenAsync(ImagenDTO imagenDTO);
Task UpdateImagenAsync(ImagenDTO imagenDTO);
Task DeleteImagenAsync(int id);
}
