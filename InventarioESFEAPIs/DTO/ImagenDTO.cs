using System;
using System.ComponentModel.DataAnnotations;

namespace InventarioESFEAPIs.DTO;

public class ImagenDTO
{
    public int Id { get; set; }

    public string? Url { get; set; } // Acepta null

    public string? ImageData { get; set; } // Acepta null

    [Required]
    public int IdAsignacionCodigo { get; set; } // Este campo es requerido
}
