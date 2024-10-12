using System;

namespace InventarioESFEAPIs.Models;

public class Imagen
{
    public int Id { get; set; }
public string Url { get; set; }
public int IdAsignacionCodigo { get; set; }
public byte[] ImageData { get; set; }
public AsignacionCodigo AsignacionCodigo { get; set; }

}
