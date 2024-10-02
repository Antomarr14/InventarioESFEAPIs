using System;

namespace InventarioESFEAPIs.Models;

public class Categoria
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int IdEstado { get; set; }
}
